using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace GameFrame.Core.Clickables;
/// <summary>
/// A <see cref="Twig{TChild}"/> that is clickable.
/// </summary>
/// <param name="child"><inheritdoc cref="Twig{TChild}.Twig" path="/param[@name='child']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class ClickableTwig<TChild>(TChild child, IComponent? parent = null):
	GeometricTwig<TChild>(child, parent), IClick where TChild : IComponent
{
	public event ClickablePressedHandler? Pressed;
	public event ClickableReleasedHandler? Released;
	public event ClickableHoveredHandler? Hovered;
	public event ClickableUnhoveredHandler? Unhovered;

	public bool Down { get; private set; } = false;

	public bool Hovering { get; private set; } = false;

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
				if(OnHovered() && Hovered is not null) Hovered(this, new(mouse.Position));
			}
			if(mouse.LeftDown && !Down)
			{
				Down = true;
				pressedTime = time;
				if(OnPressed(Mouse.Buttons.Left, mouse.Position) && Pressed is not null)
				{
					Pressed(this, new(Mouse.Buttons.Left, mouse.Position));
				}
			}
			else if(!mouse.LeftDown && Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(this, new(Mouse.Buttons.Left, mouse.Position, dt));
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
			if(Down)
			{
				Down = false;
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(OnReleased(Mouse.Buttons.Left, mouse.Position, dt) && Released is not null)
				{
					Released(this, new(Mouse.Buttons.Left, mouse.Position, dt));
				}
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
}