using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An text-based button that changes color while it is being hovered over.
/// </summary>
/// <param name="font"><inheritdoc cref="TextButton.TextButton" path="/param[@name='font']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
public class HoverTextButton(SpriteFont font, IComponent? parent = null, int layer = 0) : TextButton(font, parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// The button text color when it is being hovered over.
	/// </summary>
	public Color HoverColor { get; set; }
	/// <summary>
	/// The button text color when it is not being hovered over.
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
