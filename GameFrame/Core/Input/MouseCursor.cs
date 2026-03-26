using GameFrame.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Input;
public class MouseCursor(Texture2D texture) : GeometricLeaf, IMouseCursor
{
	public bool Centered { get; set; } = false;
	public Texture2D Texture { get; set; } = texture;
	public Color Color { get; set; } = Color.White;
	public Point Position
	{
		get => Centered ? Center : new(X, Y);
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Texture, Geometry, Color);
	}

	public void Move(int x, int y)
	{
		if(Centered)
		{
			SetCenter(x, y);
		}
		else
		{
			X = x;
			Y = y;
		}
	}

	public void Move(Point p)
	{
		if(Centered)
		{
			SetCenter(p);
		}
		else
		{
			X = p.X;
			Y = p.Y;
		}
	}

	public void Move(Vector2 p)
	{
		if(Centered)
		{
			SetCenter((int)p.X, (int)p.Y);
		}
		else
		{
			X = (int)p.X;
			Y = (int)p.Y;
		}
	}
}
