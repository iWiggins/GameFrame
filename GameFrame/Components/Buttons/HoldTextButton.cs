using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class HoldTextButton(SpriteFont font, IComponent? parent = null, int layer = 0) : TextButton(font, parent, layer), IInitialize
{
	public Color HoldColor { get; set; }
	public Color NormalColor { get; set; }

	public void Initialize() => Color = NormalColor;

	protected override bool OnPressed(Mouse.Buttons button, Point position)
	{
		Color = HoldColor;
		return true;
	}
	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)
	{
		Color = NormalColor;
		return true;
	}
}
