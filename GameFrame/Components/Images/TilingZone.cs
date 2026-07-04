using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Images;
/// <summary>
/// Draws a texture in a tiling style over a zone.
/// </summary>
/// <param name="texture"><inheritdoc cref="Texture" path="/summary"/></param>
/// <param name="effect"><inheritdoc cref="Effect" path="/summary"/></param>
/// <param name="parent"><inheritdoc cref="GeometricLeaf" path="/param[@name='parent']"/></param>
public class TilingZone(Texture2D texture, Effect? effect = null, IComponent? parent = null) : GeometricLeaf(parent), IDraw, IDrawZone
{
	/// <summary>
	/// The color for the textures in the tiling zone.
	/// </summary>
	public Color Color { get; set; } = Color.White;
	/// <summary>
	/// The textures to draw.
	/// </summary>
	public Texture2D Texture { get; set; } = texture;
	/// <summary>
	/// An optional effect to use.
	/// </summary>
	public Effect? Effect { get; set; } = effect;
	/// <summary>
	/// How much to scale the textures.
	/// </summary>
	public float Scale { get; set; } = 1;
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(texture: Texture, destinationRectangle: Geometry, color: Color);
	}
	public void StartDrawing(SpriteBatch spriteBatch)
	{
		Matrix matrix = Matrix.CreateScale(Scale);
		spriteBatch.Begin(samplerState: SamplerState.LinearWrap,effect:Effect, transformMatrix:matrix);
	}
	public void EndDrawing(SpriteBatch spriteBatch)
	{
		spriteBatch.End();
	}
}
