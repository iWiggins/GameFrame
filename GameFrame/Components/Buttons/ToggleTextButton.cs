using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// A text based button that toggles between two states when clicked.
/// </summary>
/// <param name="font"><inheritdoc cref="TextButton" path="/param[@name='font']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class ToggleTextButton(SpriteFont font, IComponent? parent = null) : TextButton(font, parent), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// The color of the button's text when it is on.
	/// </summary>
	public Color OnColor { get; set; }
	/// <summary>
	/// The color of the button's text when it is off.
	/// </summary>
	public Color OffColor { get; set; }

	/// <summary>
	/// <inheritdoc cref="ToggleImageButton.On" path="/summary"/>
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
	/// <inheritdoc cref="ToggleImageButton.Off" path="/summary"/>
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
