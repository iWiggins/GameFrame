using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Text;

/// <summary>
/// A simple drawable Text component.
/// </summary>
/// <param name="font"><inheritdoc cref="Text.Font" path="/summary"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class Text(SpriteFont font, IComponent? parent = null) : Leaf(parent), IDraw
{
	/// <summary>
	/// The font used while drawing the text.
	/// </summary>
	public SpriteFont Font { get; set; } = font;
	/// <summary>
	/// The string data being drawn.
	/// </summary>
	public string Contents { get; set; } = "";
	/// <summary>
	/// Scale transformation to increase or decrease the size of the rendered image.
	/// </summary>
	public Vector2 Scale { get; set; } = Vector2.One;
	/// <summary>
	/// The color to draw the text in.
	/// </summary>
	public Color Color { get; set; } = Color.Black;
	/// <summary>
	/// The position on screen to draw the text.
	/// </summary>
	/// <remarks>
	/// This is the top left corner of the text.
	/// </remarks>
	public Vector2 Position { get; set; } = Vector2.Zero;
	/// <summary>
	/// Text effects to use while drawing.
	/// </summary>
	public SpriteEffects Effect { get; set; } = SpriteEffects.None;

	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.DrawString(
			Font,
			Contents,
			Position,
			Color,
			0.0f, // rotation, TODO: Implement rotation
			Vector2.Zero, // origin, TODO: Implement rotation
			Scale,
			Effect,
			0.0f
			);
	}
}
