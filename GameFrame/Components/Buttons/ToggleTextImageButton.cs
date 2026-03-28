using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// A button with both text and an image that toggles between two states when clicked.
/// </summary>
/// <param name="font"><inheritdoc cref="TextButton" path="/param[@name='font']"/></param>
/// <param name="texture"><inheritdoc cref="ImageButton" path="/param[@name='texture']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
public class ToggleTextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null, int layer = 0) : TextImageButton(font, texture, parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// <inheritdoc cref="ToggleImageButton.OnColor" path="/summary"/>
	/// </summary>
	public Color OnColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="ToggleImageButton.OffColor" path="/summary"/>
	/// </summary>
	public Color OffColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="ToggleTextButton.OnColor" path="/summary"/>
	/// </summary>
	public Color ImageOnColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="ToggleTextButton.OffColor" path="/summary"/>
	/// </summary>
	public Color ImageOffColor { get; set; }

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
