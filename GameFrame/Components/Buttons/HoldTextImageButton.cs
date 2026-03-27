using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class HoldTextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null, int layer = 0) : TextImageButton(font, texture, parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;
	public Color TextHoverColor { get; set; }
	public Color TextNormalColor { get; set; }
	public Color HoverColor { get; set; }
	public Color NormalColor { get; set; }

	public void Initialize()
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
		Initialized = true;
	}
	protected override bool OnPressed(Mouse.Buttons button, Point position)  
	{
		Color = HoverColor;
		TextColor = TextHoverColor;
		return true;
	}
	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)  
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
		return true;
	}
}
