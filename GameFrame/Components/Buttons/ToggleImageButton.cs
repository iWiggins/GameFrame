using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An image based button that toggles between two states when clicked.
/// </summary>
/// <param name="texture"><inheritdoc cref="ImageButton" path="/param[@name='texture']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
public class ToggleImageButton(Texture2D texture, IComponent? parent = null, int layer = 0) : ImageButton(texture, parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// The button color while it is on.
	/// </summary>
	public Color OnColor { get; set; }
	/// <summary>
	/// The button color while it is off.
	/// </summary>
	public Color OffColor { get; set; }
	
	/// <summary>
	/// Whether the button is on.
	/// </summary>
	public bool On
	{
		get => _on;
		set
		{
			_on = value;
			SetColor();
		}
	}

	/// <summary>
	/// Whether the button is off.
	/// </summary>
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
