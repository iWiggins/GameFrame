using GameFrame.Core.Components;
using GameFrame.Core.EventHandlers;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace GameFrame.Core.Clickables;
/// <summary>
/// A <see cref="Twig{TChild}"/> that is clickable.
/// </summary>
public abstract class ClickableTwig<TChild> : GeometricTwig<TChild>, IClick where TChild : IComponent
{
	public event MouseDownHandler? Pressed;
	public event MouseUpHandler? Released;
	public event MouseHoverHandler? Hovered;
	public event MouseUnhoverHandler? Unhovered;

	protected ClickableTwig(TChild child, IComponent? parent = null, int layer = 0) :
		base(child, parent, layer)
	{ }

	public bool Down { get; private set; } = false;

	public bool Hovering { get; private set; } = false;

	public void Click(GameTime time, Mouse mouse)
	{
		if(Overlaps(mouse.Position))
		{
			if(!Hovering)
			{
				Hovering = true;
				hoverTime = time;
				if(Hovered is not null) Hovered();
			}
			if(mouse.LeftDown && !Down)
			{
				Down = true;
				pressedTime = time;
				if(Pressed is not null)
				{
					Pressed(Mouse.Buttons.Left, mouse.Position);
				}
			}
			else if(!mouse.LeftDown && Down)
			{
				Down = false;
				if(Released is not null)
				{
					double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
					Released(Mouse.Buttons.Left, mouse.Position, dt);
				}
			}
		}
		else
		{
			if(Hovering)
			{
				Hovering = false;
				if(Unhovered is not null)
				{
					double dt = time.TotalGameTime.TotalMilliseconds - hoverTime.TotalGameTime.TotalMilliseconds;
					Unhovered(dt);
				}
			}
			if(Down)
			{
				Down = false;
				if(Released is not null)
				{
					double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
					Released(Mouse.Buttons.Left, mouse.Position, dt);
				}
			}
		}
	}

	private GameTime pressedTime = new();
	private GameTime hoverTime = new();

	
}