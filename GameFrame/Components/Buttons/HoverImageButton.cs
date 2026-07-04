using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An image-based button that changes color while it is hovered over.
/// </summary>
/// <param name="texture"><inheritdoc cref="ImageButton.ImageButton" path="/param[@name='texture']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class HoverImageButton(Texture2D texture, IComponent? parent = null) : ImageButton(texture, parent), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// The button's color when it is hovered over.
	/// </summary>
	public Color HoverColor { get; set; }
	/// <summary>
	/// The button's color when it is not hovered over.
	/// </summary>
	public Color NormalColor { get; set; }

	public void Initialize()
	{
		Color = NormalColor;
		Initialized = true;
	}
	protected override bool OnHovered()
	{
		Color = HoverColor;
		return true;
	}
	protected override bool OnUnhovered(double dt)
	{
		Color = NormalColor;
		return true;
	}
}
