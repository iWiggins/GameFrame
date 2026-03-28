using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An text-based button that changes color while it is being held.
/// </summary>
/// <param name="font"><inheritdoc cref="TextButton.TextButton" path="/param[@name='font']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
public class HoldTextButton(SpriteFont font, IComponent? parent = null, int layer = 0) : TextButton(font, parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;
	/// <summary>
	/// The text color when the button is held.
	/// </summary>
	public Color HoldColor { get; set; }
	/// <summary>
	/// The text color when the button is not held.
	/// </summary>
	public Color NormalColor { get; set; }

	public void Initialize()
	{
		Color = NormalColor;
		Initialized = true;
	}

	protected override bool OnPressed(Mouse.Buttons button, Point position)
	{
		Color = HoldColor;
		return true;
	}
	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)
	{
		Color = NormalColor;
		return true;
	}
}
