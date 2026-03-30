using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameFrame.Components.Text;

/// <summary>
/// Text bound to a specific geometry, scaling to fit that geometry.
/// </summary>
public class BoundText : GeometricComponent
{
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

	public override IEnumerable<IComponent> Children
	{
		get
		{
			if(!valid) FitText();
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
		valid = false;
	}

	public override bool AddChild(IComponent component) => false;
	public override void Invalidate() =>
		valid = false;
	public override bool RemoveChild(IComponent component) => false;

	/// <summary>
	/// Aligns the inner text object within the boundtext.
	/// </summary>
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
				float scaleFactor = newWidth / textSize.X;
				int newHeight = (int)(scaleFactor * textSize.Y);

				// If new height would exceed parent, set new height to parent
				// height and calculate new scale factor for the width
				if(newHeight > Height)
				{
					newHeight = Height;
					scaleFactor = newHeight / textSize.Y;
					newWidth = (int)(scaleFactor * textSize.X);
				}

				_text.Scale = new(scaleFactor, scaleFactor);

				int xOffset = (Width - newWidth) / 2;
				int yOffset = (Height - newHeight) / 2;

				_text.Position = new(X + xOffset, Y + yOffset);				
			}
		}

		valid = true;
	}

	private readonly Text _text;
	private bool valid;
}
