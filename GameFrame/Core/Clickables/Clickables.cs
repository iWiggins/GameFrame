/// Clickable versions of geometric classes.
/// This code is generated and should not be manually edited.
using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Core.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Core.Clickables;

/// <summary>
/// A <see cref="Branch"/> that is clickable.
/// </summary>
/// <param name="buttons">The mouse buttons that should trigger the clickable.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class ClickableBranch(IEnumerable<Mouse.Buttons> buttons, IComponent? parent = null): GeometricBranch(parent), IClick
{
    public event ClickablePressedHandler? Pressed;
	public event ClickableReleasedHandler? Released;
	public event ClickableHoveredHandler? Hovered;
	public event ClickableUnhoveredHandler? Unhovered;

	public bool Down => DownButton is not null;
	public Mouse.Buttons? DownButton { get; private set; } = null;

	public bool Hovering { get; private set; } = false;

	public void Reset()
	{
		DownButton = null;
		Hovering = false;
	}

	public void Click(GameTime time, Mouse mouse)
	{
		if(Overlaps(mouse.Position))
		{
			if(!Hovering)
			{
				Hovering = true;
				hoverTime = time;
				if(OnHovered() && Hovered is not null) Hovered(this, new(mouse.Position));
			}
			if(DownButton is null)
			{
				foreach(var button in buttons)
				{
					if(mouse.IsDown(button))
					{
						DownButton = button;
						pressedTime = time;
						if(OnPressed(button, mouse.Position) && Pressed is not null)
						{
							Pressed(this, new(button, mouse.Position));
						}
						break;
					}
				}
			}
			else
			{
				if(!mouse.IsDown(DownButton.Value))
				{
					double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
					if(OnReleased(DownButton.Value, mouse.Position, dt) && Released is not null)
					{
						Released(this, new(DownButton.Value, mouse.Position, dt));
					}
					DownButton = null;
				}
			}
		}
		else
		{
			if(Hovering)
			{
				Hovering = false;
				double dt = time.TotalGameTime.TotalMilliseconds - hoverTime.TotalGameTime.TotalMilliseconds;
				if(OnUnhovered(dt) && Unhovered is not null)
				{
					Unhovered(this, new(mouse.Position, dt));
				}
			}
			if(DownButton is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(DownButton.Value, mouse.Position, dt) && Released is not null)
				{
					Released(this, new(DownButton.Value, mouse.Position, dt));
				}
				DownButton = null;
			}
		}
	}

	/// <summary>
	/// Overridable logic for when the component is hovered over.
	/// </summary>
	/// <returns>Whether the Hovered event should be raised.</returns>
	protected virtual bool OnHovered() => true;

	/// <summary>
	/// Overridable logic for when a pointer stops hovering the component.
	/// </summary>
	/// <param name="dt">The duration the component was hovered.</param>
	/// <returns>Whether the Unhovered event should be raised.</returns>
	protected virtual bool OnUnhovered(double dt) => true;

	/// <summary>
	/// Overridable logic for when the component is pressed.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <returns>Whether the Pressed event should be raised.</returns>
	protected virtual bool OnPressed(Mouse.Buttons button, Point position) => true;

	/// <summary>
	/// Overridable logic for when the component is released.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <param name="duration">The time the component spent down.</param>
	/// <returns>Whether the Released event should be raised.</returns>
	protected virtual bool OnReleased(Mouse.Buttons button, Point position, double duration) => true;

	private GameTime pressedTime = new();
	private GameTime hoverTime = new();
	private readonly List<Mouse.Buttons> _buttons = [.. buttons];
}
/// <summary>
/// A <see cref="Component"/> that is clickable.
/// </summary>
/// <param name="buttons">The mouse buttons that should trigger the clickable.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class ClickableComponent(IEnumerable<Mouse.Buttons> buttons, IComponent? parent = null): GeometricComponent(parent), IClick
{
    public event ClickablePressedHandler? Pressed;
	public event ClickableReleasedHandler? Released;
	public event ClickableHoveredHandler? Hovered;
	public event ClickableUnhoveredHandler? Unhovered;

	public bool Down => DownButton is not null;
	public Mouse.Buttons? DownButton { get; private set; } = null;

	public bool Hovering { get; private set; } = false;

	public void Reset()
	{
		DownButton = null;
		Hovering = false;
	}

	public void Click(GameTime time, Mouse mouse)
	{
		if(Overlaps(mouse.Position))
		{
			if(!Hovering)
			{
				Hovering = true;
				hoverTime = time;
				if(OnHovered() && Hovered is not null) Hovered(this, new(mouse.Position));
			}
			if(DownButton is null)
			{
				foreach(var button in buttons)
				{
					if(mouse.IsDown(button))
					{
						DownButton = button;
						pressedTime = time;
						if(OnPressed(button, mouse.Position) && Pressed is not null)
						{
							Pressed(this, new(button, mouse.Position));
						}
						break;
					}
				}
			}
			else
			{
				if(!mouse.IsDown(DownButton.Value))
				{
					double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
					if(OnReleased(DownButton.Value, mouse.Position, dt) && Released is not null)
					{
						Released(this, new(DownButton.Value, mouse.Position, dt));
					}
					DownButton = null;
				}
			}
		}
		else
		{
			if(Hovering)
			{
				Hovering = false;
				double dt = time.TotalGameTime.TotalMilliseconds - hoverTime.TotalGameTime.TotalMilliseconds;
				if(OnUnhovered(dt) && Unhovered is not null)
				{
					Unhovered(this, new(mouse.Position, dt));
				}
			}
			if(DownButton is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(DownButton.Value, mouse.Position, dt) && Released is not null)
				{
					Released(this, new(DownButton.Value, mouse.Position, dt));
				}
				DownButton = null;
			}
		}
	}

	/// <summary>
	/// Overridable logic for when the component is hovered over.
	/// </summary>
	/// <returns>Whether the Hovered event should be raised.</returns>
	protected virtual bool OnHovered() => true;

	/// <summary>
	/// Overridable logic for when a pointer stops hovering the component.
	/// </summary>
	/// <param name="dt">The duration the component was hovered.</param>
	/// <returns>Whether the Unhovered event should be raised.</returns>
	protected virtual bool OnUnhovered(double dt) => true;

	/// <summary>
	/// Overridable logic for when the component is pressed.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <returns>Whether the Pressed event should be raised.</returns>
	protected virtual bool OnPressed(Mouse.Buttons button, Point position) => true;

	/// <summary>
	/// Overridable logic for when the component is released.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <param name="duration">The time the component spent down.</param>
	/// <returns>Whether the Released event should be raised.</returns>
	protected virtual bool OnReleased(Mouse.Buttons button, Point position, double duration) => true;

	private GameTime pressedTime = new();
	private GameTime hoverTime = new();
	private readonly List<Mouse.Buttons> _buttons = [.. buttons];
}
/// <summary>
/// A <see cref="Leaf"/> that is clickable.
/// </summary>
/// <param name="buttons">The mouse buttons that should trigger the clickable.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class ClickableLeaf(IEnumerable<Mouse.Buttons> buttons, IComponent? parent = null): GeometricLeaf(parent), IClick
{
    public event ClickablePressedHandler? Pressed;
	public event ClickableReleasedHandler? Released;
	public event ClickableHoveredHandler? Hovered;
	public event ClickableUnhoveredHandler? Unhovered;

	public bool Down => DownButton is not null;
	public Mouse.Buttons? DownButton { get; private set; } = null;

	public bool Hovering { get; private set; } = false;

	public void Reset()
	{
		DownButton = null;
		Hovering = false;
	}

	public void Click(GameTime time, Mouse mouse)
	{
		if(Overlaps(mouse.Position))
		{
			if(!Hovering)
			{
				Hovering = true;
				hoverTime = time;
				if(OnHovered() && Hovered is not null) Hovered(this, new(mouse.Position));
			}
			if(DownButton is null)
			{
				foreach(var button in buttons)
				{
					if(mouse.IsDown(button))
					{
						DownButton = button;
						pressedTime = time;
						if(OnPressed(button, mouse.Position) && Pressed is not null)
						{
							Pressed(this, new(button, mouse.Position));
						}
						break;
					}
				}
			}
			else
			{
				if(!mouse.IsDown(DownButton.Value))
				{
					double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
					if(OnReleased(DownButton.Value, mouse.Position, dt) && Released is not null)
					{
						Released(this, new(DownButton.Value, mouse.Position, dt));
					}
					DownButton = null;
				}
			}
		}
		else
		{
			if(Hovering)
			{
				Hovering = false;
				double dt = time.TotalGameTime.TotalMilliseconds - hoverTime.TotalGameTime.TotalMilliseconds;
				if(OnUnhovered(dt) && Unhovered is not null)
				{
					Unhovered(this, new(mouse.Position, dt));
				}
			}
			if(DownButton is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(DownButton.Value, mouse.Position, dt) && Released is not null)
				{
					Released(this, new(DownButton.Value, mouse.Position, dt));
				}
				DownButton = null;
			}
		}
	}

	/// <summary>
	/// Overridable logic for when the component is hovered over.
	/// </summary>
	/// <returns>Whether the Hovered event should be raised.</returns>
	protected virtual bool OnHovered() => true;

	/// <summary>
	/// Overridable logic for when a pointer stops hovering the component.
	/// </summary>
	/// <param name="dt">The duration the component was hovered.</param>
	/// <returns>Whether the Unhovered event should be raised.</returns>
	protected virtual bool OnUnhovered(double dt) => true;

	/// <summary>
	/// Overridable logic for when the component is pressed.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <returns>Whether the Pressed event should be raised.</returns>
	protected virtual bool OnPressed(Mouse.Buttons button, Point position) => true;

	/// <summary>
	/// Overridable logic for when the component is released.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <param name="duration">The time the component spent down.</param>
	/// <returns>Whether the Released event should be raised.</returns>
	protected virtual bool OnReleased(Mouse.Buttons button, Point position, double duration) => true;

	private GameTime pressedTime = new();
	private GameTime hoverTime = new();
	private readonly List<Mouse.Buttons> _buttons = [.. buttons];
}
