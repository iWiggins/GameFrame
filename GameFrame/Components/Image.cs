using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GameFrame.Components;
public class Image(Texture2D texture, IComponent? parent = null, int layer = 0) : GeometricLeaf(parent, layer), IDraw
{
	public Texture2D Texture { get; set; } = texture;
	public Color Color { get; set; } = Color.White;
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Texture, Geometry, Color);
	}
}
