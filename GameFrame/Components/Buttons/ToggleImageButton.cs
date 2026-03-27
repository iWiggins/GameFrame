using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class ToggleImageButton(Texture2D texture, IComponent? parent = null, int layer = 0) : ImageButton(texture, parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;
	public Color OnColor { get; set; }
	public Color OffColor { get; set; }
	
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
		Color = OffColor;
		Initialized = true;
	}
	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)
	{
		_on = !_on;
		SetColor();
		return true;
	}

	private void SetColor() =>
		Color = _on ? OnColor : OffColor;

	private bool _on;
}
