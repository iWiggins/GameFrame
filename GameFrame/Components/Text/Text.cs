using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Text;

/// <summary>
/// A simple Text object, supporting basic customization.
/// </summary>
public class Text(SpriteFont font, IComponent? parent = null, int layer = 0) : Leaf(parent, layer), IDraw
{
	public SpriteFont Font { get; set; } = font;
	public string Contents { get; set; } = "";
	public Vector2 Scale { get; set; } = Vector2.One;
	public Color Color { get; set; } = Color.Black;
	public Vector2 Position { get; set; } = Vector2.Zero;
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
