using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Components.Text;

/// <summary>
/// Text bound to a specific geometry, scaling to fit that geometry.
/// </summary>
public class BoundText : GeometricComponent
{
	public enum VerticalAlignment
	{
		Top,
		Center,
		Bottom
	}

	public VerticalAlignment VerticalAlign { get; set; } = VerticalAlignment.Center;

	/// <summary>
	/// <inheritdoc cref="Text.Font" path="/summary"/>
	/// </summary>
	public SpriteFont Font
	{
		get => _text.Font;
		set => _text.Font = value;
	}
	/// <summary>
	/// <inheritdoc cref="Text.Contents" path="/summary"/>
	/// </summary>
	public string Contents
	{
		get => _text.Contents;
		set => _text.Contents = value;
	}
	/// <summary>
	/// <inheritdoc cref="Text.Color" path="/summary"/>
	/// </summary>
	public Color Color
	{
		get => _text.Color;
		set => _text.Color = value;
	}
	/// <summary>
	/// <inheritdoc cref="Text.Effect" path="/summary"/>
	/// </summary>
	public SpriteEffects Effect
	{
		get => _text.Effect;
		set => _text.Effect = value;
	}
	/// <summary>
	/// The proportional space between the sides of the component and the text.
	/// </summary>
	public double Margins
	{
		set
		{
			Invalidate();
			_marginLeft = _marginRight = _marginTop = _marginBottom = value;
		}
	}
	/// <summary>
	/// The proportional space between the left of the component and the text.
	/// </summary>

	public double MarginLeft
	{
		get => _marginLeft;
		set
		{
			Invalidate();
			_marginLeft = value;
		}
	}
	/// <summary>
	/// The proportional space between the right of the component and the text.
	/// </summary>
	public double MarginRight
	{ 
		get => _marginRight;
		set
		{
			Invalidate();
			_marginRight = value;
		}
	}
	/// <summary>
	/// The proportional space between the top of the component and the text.
	/// </summary>
	public double MarginTop
	{
		get => _marginTop;
		set
		{
			Invalidate();
			_marginTop = value;
		}
	}
	/// <summary>
	/// The proportional space between the bottom of the component and the text.
	/// </summary>
	public double MarginBottom
	{
		get => _marginBottom;
		set
		{
			Invalidate();
			_marginBottom = value;
		}
	}

	public override IEnumerable<IComponent> Children
	{
		get
		{
			if(!_valid) FitText();
			yield return _text;
		}
	}

	public override bool HasChildren => true;

	/// <param name="font"><inheritdoc cref="Font" path="/summary"/></param>
	/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
	/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
	public BoundText(SpriteFont font, IComponent? parent = null):
		base(parent)
	{
		_text = new(font, this);
		_valid = false;
	}

	public override bool AddChild(IComponent component) => false;
	public override void Invalidate()
	{
		_valid = false;
		_text.Invalidate();
	}
	public override bool RemoveChild(IComponent component) => false;

	/// <summary>
	/// Aligns the inner text object within the boundtext.
	/// </summary>
	public void FitText()
	{
		if(!_text.IsEmpty)
		{
			Vector2 textSize = _text.CalculateMeasure();

			// if Either dimension is 0, do nothing.
			if(textSize.X != 0 && textSize.Y != 0)
			{
				int leftMargin = (int)(Width * _marginLeft);
				int rightMargin = (int)(Width * _marginRight);
				int topMargin = (int)(Width * _marginTop);
				int bottomMargin = (int)(Width * _marginBottom);

				int effectiveWidth = Width - (leftMargin + rightMargin);
				int effectiveHeight = Height - (topMargin + bottomMargin);

				int effectiveX = X + leftMargin;

				int effectiveY = Y + topMargin;

				// by default, attempt to scale width first
				int newWidth = effectiveWidth;
				float scaleFactor = newWidth / textSize.X;
				int newHeight = (int)(scaleFactor * textSize.Y);

				// If new height would exceed parent, set new height to parent
				// height and calculate new scale factor for the width
				if(newHeight > effectiveHeight)
				{
					newHeight = effectiveHeight;
					scaleFactor = newHeight / textSize.Y;
					newWidth = (int)(scaleFactor * textSize.X);
				}

				_text.Scale = new(scaleFactor, scaleFactor);

				int xOffset = VerticalAlign switch
				{
					VerticalAlignment.Top => topMargin,
					VerticalAlignment.Center => (effectiveWidth - newWidth) / 2,
					VerticalAlignment.Bottom => effectiveWidth - newWidth,
					_ => throw new InvalidOperationException("Invalid BoundText alignment.")
				};
				int yOffset = (effectiveHeight - newHeight) / 2;

				_text.Position = new(effectiveX + xOffset, effectiveY + yOffset);				
			}
		}

		_valid = true;
	}

	private readonly Text _text;
	private bool _valid;
	private double _marginLeft = 0;
	private double _marginRight = 0;
	private double _marginTop = 0;
	private double _marginBottom = 0;
}
