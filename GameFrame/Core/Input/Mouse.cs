using GameFrame.Components;
using GameFrame.Core;
using GameFrame.Core.EventHandlers;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameFrame.Core.Input;

/// <summary>
/// A component that manages the mouse, and optionally a mouse pointer.
/// </summary>
/// <param name="root">The root component of the frame this mouse belongs to.</param>
public class Mouse(IRoot root) : IComponent, IUpdate, IReset
{
	public enum Buttons
	{
		Left,
		Right
	}

	public event MouseDownHandler? LeftPressed;
	public event MouseUpHandler? LeftReleased;

	public event MouseDownHandler? RightPressed;
	public event MouseUpHandler? RightReleased;

	public Point Position { get; private set; }
	public bool LeftDown { get; private set; }
	public bool RightDown { get; private set; }

	public IComponent? Parent => root;

	public ulong Id { get; } = Identity.GenerateId();

	public int Layer
	{
		get => int.MinValue;
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

	public bool AddChild(IComponent component) => false;
	public bool RemoveChild(IComponent component) => false;
	public void Invalidate() => Cursor?.Invalidate();

	public void Reset()
	{
		LeftDown = false;
		RightDown = false;
	}

	public void Update(GameTime time)
	{
		MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();

		Position = state.Position;

		Cursor?.Move(Position);

		bool leftPressed = state.LeftButton == ButtonState.Pressed;
		bool rightPressed = state.RightButton == ButtonState.Pressed;

		if(leftPressed && !LeftDown)
		{
			LeftDown = true;
			LeftPressedOn = time;
			if(LeftPressed is not null)
			{
				LeftPressed(Buttons.Left, Position);
			}
		}
		else if(!leftPressed && LeftDown)
		{
			LeftDown = false;
			if(LeftReleased is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - LeftPressedOn.TotalGameTime.TotalMilliseconds;
				LeftReleased(Buttons.Left, Position, dt);
			}
		}

		if(rightPressed && !RightDown)
		{
			RightDown = true;
			RightPressedOn = time;
			if(RightPressed is not null)
			{
				RightPressed(Buttons.Right, Position);
			}
		}
		else if(!rightPressed && RightDown)
		{
			RightDown = false;
			if(RightReleased is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - RightPressedOn.TotalGameTime.TotalMilliseconds;
				RightReleased(Buttons.Right, Position, dt);
			}
		}
	}

	private GameTime LeftPressedOn = new();
	private GameTime RightPressedOn = new();
}
