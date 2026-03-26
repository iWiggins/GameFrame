using GameFrame.Core.Clickables;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class ImageButton(Texture2D texture, IComponent? parent = null, int layer = 0) : ClickableTwig<Image>(new(texture), parent, layer), IButton
{
	public Texture2D Texture
	{
		get => Child.Texture;
		set => Child.Texture = value;
	}
	public Color Color
	{
		get => Child.Color;
		set => Child.Color = value;
	}
}
