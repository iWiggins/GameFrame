using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class ToggleTextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null, int layer = 0) : TextImageButton(font, texture, parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;
	public Color OnColor { get; set; }
	public Color OffColor { get; set; }
	public Color ImageOnColor { get; set; }
	public Color ImageOffColor { get; set; }
	public bool On
	{
		get => _on;
		set
		{
			_on = value;
			SetColor();
		}
	}

	public bool Off
	{
		get => !_on;
		set
		{
			_on = !value;
			SetColor();
		}
	}

	public void Initialize()
	{
		Color = ImageOffColor;
		TextColor = OffColor;
		Initialized = true;
	}
	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)
	{
		_on = !_on;
		SetColor();
		return true;
	}

	private void SetColor()
	{
		if(_on)
		{
			Color = ImageOnColor;
			TextColor = OnColor;
		}
		else
		{
			Color = ImageOffColor;
			TextColor = OffColor;
		}
	}

	private bool _on;
}
