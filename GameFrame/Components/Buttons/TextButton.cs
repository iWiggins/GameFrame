using GameFrame.Components.Text;
using GameFrame.Core.Clickables;
using GameFrame.Core.Components;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An text-based button, exposing mouse events.
/// </summary>
/// <param name="font">The font for the button's text.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class TextButton(SpriteFont font, IComponent? parent = null):
	ClickableTwig<BoundText>(new(font), null, parent), IButton
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
	/// <summary>
	/// <inheritdoc cref="BoundText.Margins" path="/summary"/>
	/// </summary>
	public double Margins
	{
		set => Child.Margins = value;
	}
	/// <summary>
	/// <inheritdoc cref="BoundText.MarginLeft" path="/summary"/>
	/// </summary>
	public double MarginLeft
	{
		get => Child.MarginLeft;
		set => Child.MarginLeft = value;
	}
	/// <summary>
	/// <inheritdoc cref="BoundText.MarginRight" path="/summary"/>
	/// </summary>
	public double MarginRight
	{
		get => Child.MarginRight;
		set => Child.MarginRight = value;
	}
	/// <summary>
	/// <inheritdoc cref="BoundText.MarginTop" path="/summary"/>
	/// </summary>
	public double MarginTop
	{
		get => Child.MarginTop;
		set => Child.MarginTop = value;
	}
	/// <summary>
	/// <inheritdoc cref="BoundText.MarginBottom" path="/summary"/>
	/// </summary>
	public double MarginBottom
	{
		get => Child.MarginBottom;
		set => Child.MarginBottom = value;
	}
}
