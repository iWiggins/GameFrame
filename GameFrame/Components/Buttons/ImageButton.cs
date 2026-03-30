using GameFrame.Components.Images;
using GameFrame.Core.Clickables;
using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An image-based button, exposing mouse events.
/// </summary>
/// <param name="texture">A texture for the button image.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class ImageButton(Texture2D texture, IComponent? parent = null):
	ClickableTwig<Image>(new(texture), parent), IButton
{
	/// <summary>
	/// The button image's texture.
	/// </summary>
	public Texture2D Texture
	{
		get => Child.Texture;
		set => Child.Texture = value;
	}
	/// <summary>
	/// The button image's color.
	/// </summary>
	public Color Color
	{
		get => Child.Color;
		set => Child.Color = value;
	}
}
