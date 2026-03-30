using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// A button with both an image and text that changes color as it is hovered over.
/// </summary>
/// <param name="font"><inheritdoc cref="HoldTextButton" path="/param[@name='font']"/></param>
/// <param name="texture"><inheritdoc cref="HoldImageButton" path="/param[@name='texture']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class HoverTextImageButton(SpriteFont font, Texture2D texture, IComponent? parent = null) : TextImageButton(font, texture, parent), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// <inheritdoc cref="HoverTextButton.HoldColor" path="/summary"/>
	/// </summary>
	public Color TextHoverColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="HoverTextButton.NormalColor" path="/summary"/>
	/// </summary>
	public Color TextNormalColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="HoverImageButton.HoldColor" path="/summary"/>
	/// </summary>
	public Color HoverColor { get; set; }
	/// <summary>
	/// <inheritdoc cref="HoverImageButton.NormalColor" path="/summary"/>
	/// </summary>
	public Color NormalColor { get; set; }

	public void Initialize()
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
		Initialized = true;
	}
	protected override bool OnHovered()
	{
		Color = HoverColor;
		TextColor = TextHoverColor;
		return true;
	}
	protected override bool OnUnhovered(double dt)
	{
		Color = NormalColor;
		TextColor = TextNormalColor;
		return true;
	}
}
