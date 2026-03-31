using GameFrame.Components.Images;
using GameFrame.Components.Text;
using GameFrame.Core.Clickables;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameFrame.Components.Buttons;
/// <summary>
/// A button with both an image and text.
/// </summary>
/// <param name="font"><inheritdoc cref="TextButton.TextButton" path="/param[@name='font']"/></param>
/// <param name="texture"><inheritdoc cref="ImageButton.ImageButton" path="/param[@name='texture']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class TextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null):
	ClickableComponent([Mouse.Buttons.Left], parent), IButton
{
	/// <summary>
	/// <inheritdoc cref="TextButton.Font"/>
	/// </summary>
	public SpriteFont Font
	{
		get => _text.Font;
		set => _text.Font = value;
	}
	/// <summary>
	/// <inheritdoc cref="TextButton.Contents"/>
	/// </summary>
	public string Contents
	{
		get => _text.Contents;
		set => _text.Contents = value;
	}
	/// <summary>
	/// <inheritdoc cref="ImageButton.Color"/>
	/// </summary>
	public Color Color
	{
		get => _background.Color;
		set => _background.Color = value;
	}
	/// <summary>
	/// <inheritdoc cref="TextButton.Color"/>
	/// </summary>
	public Color TextColor
	{
		get => _text.Color;
		set => _text.Color = value;
	}
	/// <summary>
	/// <inheritdoc cref="TextButton.Effect"/>
	/// </summary>
	public SpriteEffects TextEffect
	{
		get => _text.Effect;
		set => _text.Effect = value;
	}
	public override bool AddChild(IComponent component) => false;
	public override bool RemoveChild(IComponent component) => false;
	public override void Invalidate()
	{
		_valid = false;
		_background.Invalidate();
		_text.Invalidate();
	}
	/// <summary>
	/// Force the image and text to snap to the button's geometry.
	/// </summary>
	public void FitChildren()
	{
		_background.Geometry = Geometry;
		_text.Geometry = Geometry;
		_valid = true;
	}

	public override IEnumerable<IComponent> Children
	{
		get
		{
			if(!_valid) FitChildren();
			yield return _background;
			yield return _text;
		}
	}

	public override bool HasChildren => true;

	private readonly Image _background = new(texture);
	private readonly BoundText _text = new(font);

	bool _valid = false;
}
