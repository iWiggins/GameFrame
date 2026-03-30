using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameFrame.Components.Images;
/// <summary>
/// A component that draws a 2d image.
/// </summary>
/// <param name="texture"><inheritdoc cref="Texture" path="/summary"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class Image(Texture2D texture, IComponent? parent = null) : GeometricLeaf(parent), IDraw
{
	/// <summary>
	/// The texture to draw.
	/// </summary>
	public Texture2D Texture { get; set; } = texture;
	/// <summary>
	/// The image color.
	/// </summary>
	public Color Color { get; set; } = Color.White;
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Texture, Geometry, Color);
	}
}
