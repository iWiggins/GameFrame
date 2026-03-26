using GameFrame.Components.Text;
using GameFrame.Core.Clickables;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class TextButton(SpriteFont font, IComponent? parent = null, int layer = 0) : ClickableTwig<BoundText>(new(font), parent, layer), IButton
{
	public SpriteFont Font
	{
		get => Child.Font;
		set => Child.Font = value;
	}
	public string Contents
	{
		get => Child.Contents;
		set => Child.Contents = value;
	}
	public Color Color
	{
		get => Child.Color;
		set => Child.Color = value;
	}
	public SpriteEffects Effect
	{
		get => Child.Effect;
		set => Child.Effect = value;
	}
}
