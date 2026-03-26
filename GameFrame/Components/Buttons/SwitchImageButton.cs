using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
public class SwitchImageButton(Texture2D offTexture, Texture2D onTexture, IComponent? parent = null, int layer = 0) : ImageButton(offTexture, parent, layer), IInitialize
{
	public Color OnColor { get; set; }
	public Color OffColor { get; set; }
	public Texture2D OnTexture { get; set; } = onTexture;
	public Texture2D OffTexture { get; set; } = offTexture;

	public bool On
	{
		get => _on;
		set
		{
			_on = value;
			SetImage();
		}
	}

	public bool Off
	{
		get => !_on;
		set
		{
			_on = !value;
			SetImage();
		}
	}

	public void Initialize() => Color = OffColor;
	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)
	{
		_on = !_on;
		SetImage();
		return true;
	}

	private void SetImage()
	{
		if(_on)
		{
			Color = OnColor;
			Texture = OnTexture;
		}
		else
		{
			Color = OffColor;
			Texture = OffTexture;
		}
	}

	private bool _on;
}
