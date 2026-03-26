using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class HoverImageButton(Texture2D texture, IComponent? parent = null, int layer = 0) : ImageButton(texture, parent, layer), IInitialize
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
