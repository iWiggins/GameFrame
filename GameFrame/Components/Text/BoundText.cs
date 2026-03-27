using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameFrame.Components.Text;

/// <summary>
/// Text bound to a specific geometry, scaling to fit that geometry.
/// </summary>
public class BoundText : Component, IGeometric
{
	public Rectangle Geometry
	{
		get => _geometry;
		set
		{
			Invalidate();
			_geometry = value;
		}
	}
	public int X
	{
		get => _geometry.X;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Y
	{
		get => _geometry.Y;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public int Width
	{
		get => _geometry.Width;
		set
		{
			Invalidate();
			_geometry.Width = value;
		}
	}
	public int Height
	{
		get => _geometry.Height;
		set
		{
			Invalidate();
			_geometry.Height = value;
		}
	}
	public Point Center
	{
		get => _geometry.Center;
		set => SetCenter(value);
	}
	public int Left
	{
		get => _geometry.Left;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Right
	{
		get => _geometry.Right;
		set
		{
			Invalidate();
			_geometry.X = value - _geometry.Width;
		}
	}
	public int Top
	{
		get => _geometry.Top;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public int Bottom
	{
		get => _geometry.Bottom;
		set
		{
			Invalidate();
			_geometry.Y = value - _geometry.Height;
		}
	}
	public SpriteFont Font
	{
		get => _text.Font;
		set => _text.Font = value;
	}
	public string Contents
	{
		get => _text.Contents;
		set => _text.Contents = value;
	}
	public Color Color
	{
		get => _text.Color;
		set => _text.Color = value;
	}
	public SpriteEffects Effect
	{
		get => _text.Effect;
		set => _text.Effect = value;
	}

	public override IEnumerable<IComponent> Children
	{
		get
		{
			if(!valid) FitText();
			yield return _text;
		}
	}

	public override bool HasChildren => true;

	public BoundText(SpriteFont font, IComponent? parent = null, int layer = 0):
		base(parent, layer)
	{
		_text = new(font, this, layer);
		valid = false;
	}

	public override bool AddChild(IComponent component) => false;
	public override void Invalidate() =>
		valid = false;
	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);
	public override bool RemoveChild(IComponent component) => false;
	public void SetCenter(Point p)
	{
		Invalidate();
		var location = _geometry.MoveCenterpoint(p);
		_geometry.Location = location;
	}
	public void SetCenter(int x, int y)
	{
		Invalidate();
		var location = _geometry.MoveCenterpoint(x, y);
		_geometry.Location = location;
	}

	public void FitText()
	{
		if(Contents != "")
		{
			Vector2 textSize = Font.MeasureString(Contents);

			// if Either dimension is 0, do nothing.
			if(textSize.X != 0 && textSize.Y != 0)
			{
				// by default, attempt to scale width first
				int newWidth = Width;
				float scaleFactor = newWidth / textSize.Y;
				int newHeight = (int)(scaleFactor * textSize.X);

				// If new height would exceed parent, set new height to parent
				// height and calculate new scale factor for the width
				if(newHeight > Height)
				{
					newHeight = Height;
					scaleFactor = newHeight / textSize.X;
					newWidth = (int)(scaleFactor * textSize.Y);
				}

				_text.Scale = new(scaleFactor, scaleFactor);

				int xOffset = newWidth / 3;
				int yOffset = newHeight / 4;

				_text.Position = new(X + xOffset, Y + yOffset);				
			}
		}

		valid = true;
	}

	private Rectangle _geometry;
	private readonly Text _text;
	private bool valid;
}
