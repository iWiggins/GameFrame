using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class HoverTextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null, int layer = 0) : TextImageButton(font, texture, parent, layer), IInitialize
{
	public Color TextHoverColor { get; set; }
	public Color TextNormalColor { get; set; }
	public Color HoverColor { get; set; }
	public Color NormalColor { get; set; }

	public void Initialize()
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
	}
	protected override bool OnHovered()
	{
		Color = HoverColor;
		TextColor = TextHoverColor;
		return true;
	}
	protected override bool OnUnhovered(double dt)
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
		return true;
	}
}
