using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GameFrame.Core.Input;

/// <summary>
/// A component that manages the mouse, and optionally a mouse pointer.
/// </summary>
/// <param name="root">The root component of the frame this mouse belongs to.</param>
public class Mouse(IRoot root) : IComponent, IUpdate, IInitialize, IReset
{
	public enum Buttons
	{
		Left,
		Right
	}

	/// <summary>
	/// Arguments for a mouse down event.
	/// </summary>
	/// <param name="button"><inheritdoc cref="Button" path="/summary"/></param>
	/// <param name="position"><inheritdoc cref="Position" path="/summary"/></param>
	public class MouseDownEventArgs(Buttons button, Point position) : EventArgs
	{
		/// <summary>
		/// Which mouse button was pressed.
		/// </summary>
		public Buttons Button { get; } = button;
		/// <summary>
		/// The position when the mouse was pressed.
		/// </summary>
		public Point Position { get; } = position;
	}

	/// <summary>
	/// Arguments for a mouse up event.
	/// </summary>
	/// <param name="button"><inheritdoc cref="Button" path="/summary"/></param>
	/// <param name="position"><inheritdoc cref="Position" path="/summary"/></param>
	/// <param name="duration"><inheritdoc cref="Duration" path="/summary"/></param>
	public class MouseUpEventArgs(Buttons button, Point position, double duration) : EventArgs
	{
		/// <summary>
		/// Which button was released.
		/// </summary>
		public Buttons Button { get; } = button;
		/// <summary>
		/// The position of the cursor when released.
		/// </summary>
		public Point Position { get; } = position;
		/// <summary>
		/// How long the button was down.
		/// </summary>
		public double Duration { get; } = duration;
	}

	/// <summary>
	/// Arguments for a mouse moved event.
	/// </summary>
	/// <param name="oldPosition"><inheritdoc cref="OldPosition" path="/summary"/></param>
	/// <param name="newPosition"><inheritdoc cref="NewPosition" path="/summary"/></param>
	public class MouseMovedEventArgs(Point oldPosition, Point newPosition)
	{
		/// <summary>
		/// The previous position of the mouse.
		/// </summary>
		public Point OldPosition { get; } = oldPosition;
		/// <summary>
		/// The new position of the mouse.
		/// </summary>
		public Point NewPosition { get; } = newPosition;
	}

	/// <summary>
	/// Handler for a mouse down event.
	/// </summary>
	public delegate void MouseDownHandler(IComponent? sender, MouseDownEventArgs args);

	/// <summary>
	/// Handler for a mouse up event.
	/// </summary>
	public delegate void MouseUpHandler(IComponent? sender, MouseUpEventArgs args);

	/// <summary>
	/// Handler for a mouse move event.
	/// </summary>
	public delegate void MouseMovedHandler(IComponent? sender, MouseMovedEventArgs args);

	/// <summary>
	/// Raised when the left mouse button is pressed.
	/// </summary>
	public event MouseDownHandler? LeftPressed;
	/// <summary>
	/// Raised when the left mouse button is released.
	/// </summary>
	public event MouseUpHandler? LeftReleased;
	/// <summary>
	/// Raised when the right mouse button is pressed.
	/// </summary>
	public event MouseDownHandler? RightPressed;
	/// <summary>
	/// Raised when the right mouse button is released.
	/// </summary>
	public event MouseUpHandler? RightReleased;
	/// <summary>
	/// Raised when the mouse is moved.
	/// </summary>
	public event MouseMovedHandler? MouseMoved;

	/// <summary>
	/// Current mouse position.
	/// </summary>
	public Point Position { get; private set; }
	/// <summary>
	/// Whether the left button is down.
	/// </summary>
	public bool LeftDown { get; private set; }
	/// <summary>
	/// Whether the right button is down.
	/// </summary>
	public bool RightDown { get; private set; }
	public bool IsDown(Buttons button) => button switch
	{ 
		Buttons.Left => LeftDown,
		Buttons.Right => RightDown,
		_ => throw new ArgumentOutOfRangeException(nameof(button))
	};
	public bool ButtonDown => LeftDown || RightDown;

	public IComponent? Parent => root;

	public ulong Id { get; } = Identity.GenerateId();

	public int Layer
	{
		get => int.MaxValue;
		set { }
	}
	public bool Enabled { get; set; } = true;

	public IEnumerable<IComponent> Children =>
		Cursor is not null ? [Cursor] : [];

	public bool HasChildren => Cursor is not null;

	/// <summary>
	/// The mouse cursor associated to this mouse.
	/// </summary>
	public IMouseCursor? Cursor { get; set; }

	public bool Initialized { get; private set; } = false;

	public bool AddChild(IComponent component) => false;
	public bool RemoveChild(IComponent component) => false;
	public void Invalidate() => Cursor?.Invalidate();

	public void Initialize()
	{
		MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();
		Position = state.Position;

		Initialized = true;
	}

	public void Reset()
	{
		LeftDown = false;
		RightDown = false;
	}

	public void Update(GameTime time)
	{
		MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();

		var newPosition = state.Position;

		if(newPosition != Position)
		{
			if(MouseMoved is not null) MouseMoved(this, new(Position, newPosition));
			Position = newPosition;
			Cursor?.Move(Position);
		}

		bool leftPressed = state.LeftButton == ButtonState.Pressed;
		bool rightPressed = state.RightButton == ButtonState.Pressed;

		if(leftPressed && !LeftDown)
		{
			LeftDown = true;
			_leftPressedOn = time;
			if(LeftPressed is not null)
			{
				LeftPressed(this, new(Buttons.Left, Position));
			}
		}
		else if(!leftPressed && LeftDown)
		{
			LeftDown = false;
			if(LeftReleased is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - _leftPressedOn.TotalGameTime.TotalMilliseconds;
				LeftReleased(this, new(Buttons.Left, Position, dt));
			}
		}

		if(rightPressed && !RightDown)
		{
			RightDown = true;
			_rightPressedOn = time;
			if(RightPressed is not null)
			{
				RightPressed(this, new(Buttons.Right, Position));
			}
		}
		else if(!rightPressed && RightDown)
		{
			RightDown = false;
			if(RightReleased is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - _rightPressedOn.TotalGameTime.TotalMilliseconds;
				RightReleased(this, new(Buttons.Right, Position, dt));
			}
		}
	}

	private GameTime _leftPressedOn = new();
	private GameTime _rightPressedOn = new();
}
