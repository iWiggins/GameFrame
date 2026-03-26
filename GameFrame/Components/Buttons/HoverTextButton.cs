using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class HoverTextButton(SpriteFont font, IComponent? parent = null, int layer = 0) : TextButton(font, parent, layer), IInitialize
{
	public Color HoverColor { get; set; }
	public Color NormalColor { get; set; }

	public void Initialize() => Color = NormalColor;
	protected override bool OnHovered()
	{
		Color = HoverColor;
		return true;
	}
	protected override bool OnUnhovered(double dt)
	{
		Color = NormalColor;
		return true;
	}
}
