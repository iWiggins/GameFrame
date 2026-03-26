using GameFrame.Components.Text;
using GameFrame.Core.Clickables;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameFrame.Components.Buttons;
public class TextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null, int layer = 0) : ClickableComponent(parent, layer), IButton
{
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
		get => _background.Color;
		set => _background.Color = value;
	}
	public Color TextColor
	{
		get => _text.Color;
		set => _text.Color = value;
	}
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
