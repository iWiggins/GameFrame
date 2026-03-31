using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GameFrame.Components.Text;

/// <summary>
/// A simple drawable Text component.
/// </summary>
/// <param name="font"><inheritdoc cref="Text.Font" path="/summary"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class Text(SpriteFont font, IComponent? parent = null) : Leaf(parent), IDraw
{
	public enum HorizontalAlignment
	{
		Left,
		Center,
		Right
	}
	/// <summary>
	/// The horizontal alignment of the text.
	/// </summary>
	public HorizontalAlignment HorizontalAlign { get; set; } = HorizontalAlignment.Center;
	/// <summary>
	/// The font used while drawing the text.
	/// </summary>
	public SpriteFont Font { get; set; } = font;
	/// <summary>
	/// The string data the text represents.
	/// </summary>
	public string Contents
	{
		get => _contents;
		set
		{
			Invalidate();
			_contents = value;
			_lines.Clear();
			_lines.AddRange(StringSupport.SplitLines(value));
		}
	}
	/// <summary>
	/// Whether there the string the text represents is empty.
	/// </summary>
	public bool IsEmpty => _lines.Count <= 0;
	/// <summary>
	/// Scale transformation to increase or decrease the size of the rendered image.
	/// </summary>
	public Vector2 Scale
	{
		get => _scale;
		set
		{
			Invalidate();
			_scale = value;
		}
	}
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

	public Vector2 CalculateMeasure()
	{
		if(_valid)
		{
			return _measure;
		}
		else
		{
			float maxWidth = 0;
			float height = 0;
			foreach(var line in _lines)
			{
				var measure = Font.MeasureString(line);
				if(measure.X > maxWidth) maxWidth = measure.X;
				height += measure.Y;
			}
			_measure = new(maxWidth, height);
			return _measure;
		}
	}

	public override void Invalidate() => _valid = false;

	public void Draw(SpriteBatch spriteBatch)
	{
		var measure = CalculateMeasure();
		float y = Position.Y;
		foreach(string line in _lines)
		{
			float x = Position.X;
			Vector2 lineMeasure = Font.MeasureString(line);
			x = HorizontalAlign switch
			{
				HorizontalAlignment.Left => x,
				HorizontalAlignment.Right => x + (measure.X - lineMeasure.X),
				HorizontalAlignment.Center => x + (measure.X - lineMeasure.X) / 2,
				_ => throw new InvalidOperationException("Invalid Text alignment.")
			};

			spriteBatch.DrawString(
				Font,
				line,
				new(x,y),
				Color,
				0.0f,
				Vector2.Zero,
				Scale,
				Effect,
				0.0f
				);

			y += lineMeasure.Y;
		}
	}

	private readonly List<string> _lines = [];
	private string _contents = "";
	private Vector2 _measure;
	private Vector2 _scale = Vector2.One;
	private bool _valid = false;
}
