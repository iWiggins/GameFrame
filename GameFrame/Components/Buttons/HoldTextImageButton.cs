using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// A button with both an image and text that changes color as it is held.
/// </summary>
/// <param name="font"><inheritdoc cref="HoldTextButton" path="/param[@name='font']"/></param>
/// <param name="texture"><inheritdoc cref="HoldImageButton" path="/param[@name='texture']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class HoldTextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null) : TextImageButton(font, texture, parent), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// <inheritdoc cref="HoldTextButton.HoldColor" path="/summary"/>
	/// </summary>
	public Color TextHoldColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="HoldTextButton.NormalColor" path="/summary"/>
	/// </summary>
	public Color TextNormalColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="HoldImageButton.HoldColor" path="/summary"/>
	/// </summary>
	public Color HoldColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="HoldImageButton.NormalColor" path="/summary"/>
	/// </summary>
	public Color NormalColor { get; set; }

	public void Initialize()
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
		Initialized = true;
	}
	protected override bool OnPressed(Mouse.Buttons button, Point position)  
	{
		Color = HoldColor;
		TextColor = TextHoldColor;
		return true;
	}
	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)  
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
		return true;
	}
}
