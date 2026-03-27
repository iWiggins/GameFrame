using GameFrame.Components;
using GameFrame.Core;
using GameFrame.Core.EventHandlers;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameFrame.Core.Input;
public class Mouse: IComponent, IUpdate, IReset
{
	public enum Buttons
	{
		Left
	}

	public event MouseDownHandler? LeftPressed;
	public event MouseUpHandler? LeftReleased;

	public Point Position { get; private set; }
	public bool LeftDown { get; private set; }

	public IComponent? Parent => _root;

	public ulong Id { get; }

	public int Layer
	{
		get => int.MaxValue;
		set { }
	}
	public bool Enabled { get; set; }

	public IEnumerable<IComponent> Children
	{
		get
		{
			if(Cursor is not null) yield return Cursor;
		}
	}

	public bool HasChildren => false;

	public IMouseCursor? Cursor { get; set; }

	public Mouse(IComponent root)
	{
		Enabled = true;
		_root = root;
		Id = Identity.GenerateId();
		PressedOn = new();
	}

	public bool AddChild(IComponent component) => false;
	public bool RemoveChild(IComponent component) => false;
	public void Invalidate() { }

	public void Reset()
	{
		LeftDown = false;
	}

	public void Update(GameTime time)
	{
		MouseState state = Microsoft.Xna.Framework.Input.Mouse.GetState();

		Position = state.Position;

		Cursor?.Move(Position);

		bool pressed = state.LeftButton == ButtonState.Pressed;
		var position = state.Position;

		if(pressed && !LeftDown)
		{
			LeftDown = true;
			PressedOn = time;
			if(LeftPressed is not null)
			{
				LeftPressed(Buttons.Left, position);
			}
		}
		else if(!pressed && LeftDown)
		{
			LeftDown = false;
			if(LeftReleased is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - PressedOn.TotalGameTime.TotalMilliseconds;
				LeftReleased(Buttons.Left, position, dt);
			}
		}
	}

	private readonly IComponent _root;
	private GameTime PressedOn;
}
