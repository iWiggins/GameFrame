using GameFrame.Components.Text;
using GameFrame.Core.Clickables;
using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An text-based button, exposing mouse events.
/// </summary>
/// <param name="font">The font for the button's text.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
public class TextButton(SpriteFont font, IComponent? parent = null, int layer = 0):
	ClickableTwig<BoundText>(new(font), parent, layer), IButton
{
	/// <summary>
	/// The button text's font.
	/// </summary>
	public SpriteFont Font
	{
		get => Child.Font;
		set => Child.Font = value;
	}
	/// <summary>
	/// The text on the button.
	/// </summary>
	public string Contents
	{
		get => Child.Contents;
		set => Child.Contents = value;
	}
	/// <summary>
	/// The color of the text.
	/// </summary>
	public Color Color
	{
		get => Child.Color;
		set => Child.Color = value;
	}
	/// <summary>
	/// Effects to apply to the text.
	/// </summary>
	public SpriteEffects Effect
	{
		get => Child.Effect;
		set => Child.Effect = value;
	}
}
