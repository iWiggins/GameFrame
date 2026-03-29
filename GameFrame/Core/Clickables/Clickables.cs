/// Clickable versions of geometric classes.
/// This code is generated and should not be manually edited.
using GameFrame.Core.Components;
using GameFrame.Core.EventHandlers;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Core.Input;
using Microsoft.Xna.Framework;

namespace GameFrame.Core.Clickables;

/// <summary>
/// A <see cref="Branch"/> that is clickable.
/// </summary>
public abstract class ClickableBranch(IComponent? parent = null, int layer = 0): GeometricBranch(parent, layer), IClick
{
    public event MouseDownHandler? Pressed;
	public event MouseUpHandler? Released;
	public event MouseHoverHandler? Hovered;
	public event MouseUnhoverHandler? Unhovered;

	public bool Down { get; private set; } = false;
	public bool Hovering {get; private set; } = false;

	public void Reset()
	{
		Down = false;
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
				if(OnHovered() && Hovered is not null) Hovered();
			}
			if(mouse.LeftDown && !Down)
			{
				Down = true;
				pressedTime = time;
				if(OnPressed(Mouse.Buttons.Left, mouse.Position) && Pressed is not null)
				{
					Pressed(Mouse.Buttons.Left, mouse.Position);
				}
			}
			else if(!mouse.LeftDown && Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(Mouse.Buttons.Left, mouse.Position, dt);
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
					Unhovered(dt);
				}
			}
			if(Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(Mouse.Buttons.Left, mouse.Position, dt);
				}
			}
		}
	}

	/// <summary>
	/// Internal callback when the button is hovered.
	/// </summary>
	/// <returns>Whether the Hovered event should be raised.</returns>
	protected virtual bool OnHovered() => true;

	/// <summary>
	/// Internal callback when the button is unhovered.
	/// </summary>
	/// <param name="dt">The time the button was hovered.</param>
	/// <returns>Whether the Unhovered event should be raised.</returns>
	protected virtual bool OnUnhovered(double dt) => true;

	/// <summary>
	/// Internal callback when the button is pressed.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <returns>Whether the Pressed event should be raised.</returns>
	protected virtual bool OnPressed(Mouse.Buttons button, Point position) => true;

	/// <summary>
	/// Internal callback when the button is released.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <param name="duration">The time the button spent down.</param>
	/// <returns>Whether the Released event should be raised.</returns>
	protected virtual bool OnReleased(Mouse.Buttons button, Point position, double duration) => true;

	private GameTime pressedTime = new();
	private GameTime hoverTime = new();
}
/// <summary>
/// A <see cref="Component"/> that is clickable.
/// </summary>
public abstract class ClickableComponent(IComponent? parent = null, int layer = 0): GeometricComponent(parent, layer), IClick
{
    public event MouseDownHandler? Pressed;
	public event MouseUpHandler? Released;
	public event MouseHoverHandler? Hovered;
	public event MouseUnhoverHandler? Unhovered;

	public bool Down { get; private set; } = false;
	public bool Hovering {get; private set; } = false;

	public void Reset()
	{
		Down = false;
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
				if(OnHovered() && Hovered is not null) Hovered();
			}
			if(mouse.LeftDown && !Down)
			{
				Down = true;
				pressedTime = time;
				if(OnPressed(Mouse.Buttons.Left, mouse.Position) && Pressed is not null)
				{
					Pressed(Mouse.Buttons.Left, mouse.Position);
				}
			}
			else if(!mouse.LeftDown && Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(Mouse.Buttons.Left, mouse.Position, dt);
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
					Unhovered(dt);
				}
			}
			if(Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(Mouse.Buttons.Left, mouse.Position, dt);
				}
			}
		}
	}

	/// <summary>
	/// Internal callback when the button is hovered.
	/// </summary>
	/// <returns>Whether the Hovered event should be raised.</returns>
	protected virtual bool OnHovered() => true;

	/// <summary>
	/// Internal callback when the button is unhovered.
	/// </summary>
	/// <param name="dt">The time the button was hovered.</param>
	/// <returns>Whether the Unhovered event should be raised.</returns>
	protected virtual bool OnUnhovered(double dt) => true;

	/// <summary>
	/// Internal callback when the button is pressed.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <returns>Whether the Pressed event should be raised.</returns>
	protected virtual bool OnPressed(Mouse.Buttons button, Point position) => true;

	/// <summary>
	/// Internal callback when the button is released.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <param name="duration">The time the button spent down.</param>
	/// <returns>Whether the Released event should be raised.</returns>
	protected virtual bool OnReleased(Mouse.Buttons button, Point position, double duration) => true;

	private GameTime pressedTime = new();
	private GameTime hoverTime = new();
}
/// <summary>
/// A <see cref="Leaf"/> that is clickable.
/// </summary>
public abstract class ClickableLeaf(IComponent? parent = null, int layer = 0): GeometricLeaf(parent, layer), IClick
{
    public event MouseDownHandler? Pressed;
	public event MouseUpHandler? Released;
	public event MouseHoverHandler? Hovered;
	public event MouseUnhoverHandler? Unhovered;

	public bool Down { get; private set; } = false;
	public bool Hovering {get; private set; } = false;

	public void Reset()
	{
		Down = false;
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
				if(OnHovered() && Hovered is not null) Hovered();
			}
			if(mouse.LeftDown && !Down)
			{
				Down = true;
				pressedTime = time;
				if(OnPressed(Mouse.Buttons.Left, mouse.Position) && Pressed is not null)
				{
					Pressed(Mouse.Buttons.Left, mouse.Position);
				}
			}
			else if(!mouse.LeftDown && Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(Mouse.Buttons.Left, mouse.Position, dt);
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
					Unhovered(dt);
				}
			}
			if(Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(Mouse.Buttons.Left, mouse.Position, dt);
				}
			}
		}
	}

	/// <summary>
	/// Internal callback when the button is hovered.
	/// </summary>
	/// <returns>Whether the Hovered event should be raised.</returns>
	protected virtual bool OnHovered() => true;

	/// <summary>
	/// Internal callback when the button is unhovered.
	/// </summary>
	/// <param name="dt">The time the button was hovered.</param>
	/// <returns>Whether the Unhovered event should be raised.</returns>
	protected virtual bool OnUnhovered(double dt) => true;

	/// <summary>
	/// Internal callback when the button is pressed.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <returns>Whether the Pressed event should be raised.</returns>
	protected virtual bool OnPressed(Mouse.Buttons button, Point position) => true;

	/// <summary>
	/// Internal callback when the button is released.
	/// </summary>
	/// <param name="button">The mouse button pressed.</param>
	/// <param name="position">The position of the mouse.</param>
	/// <param name="duration">The time the button spent down.</param>
	/// <returns>Whether the Released event should be raised.</returns>
	protected virtual bool OnReleased(Mouse.Buttons button, Point position, double duration) => true;

	private GameTime pressedTime = new();
	private GameTime hoverTime = new();
}
