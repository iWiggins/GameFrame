using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Core.Input;

/// <summary>
/// A component that manages a single key on the keyboard.
/// </summary>
/// <param name="parent">The <see cref="Keyboard"/> managing this key.</param>
/// <param name="keycode">The key this component will manage.</param>
public class Key(Keyboard parent, Keys keycode) : Leaf(parent), IUpdate, IReset
{
	/// <summary>
	/// Handler for a keypress.
	/// </summary>
	/// <param name="key">The pressed key.</param>
	public delegate void KeyDownHandler(Keys key);

	/// <summary>
	/// Handler for a key release.
	/// </summary>
	/// <param name="key">The released key.</param>
	/// <param name="duration">How long the key was held.</param>
	public delegate void KeyUpHandler(Keys key, double duration);

	/// <summary>
	/// The key being managed.
	/// </summary>
	public Keys KeyCode { get; } = keycode;

	/// <summary>
	/// Whether the managed key is down.
	/// </summary>
	public bool IsDown { get; private set; } = false;

	/// <summary>
	/// Raised when the key has been pressed.
	/// </summary>
	public event KeyDownHandler? KeyPressed;

	/// <summary>
	/// Raised when the key has been released.
	/// </summary>
	public event KeyUpHandler? KeyReleased;

	public void Reset()
	{
		IsDown = false;
	}

	public void Update(GameTime time)
	{
		bool pressed = parent.IsKeyDown(KeyCode);

		if(pressed && !IsDown)
		{
			IsDown = true;
			PressedOn = time;
			if(KeyPressed is not null)
			{
				KeyPressed(KeyCode);
			}
		}
		else if(!pressed && IsDown)
		{
			IsDown = false;
			if(KeyReleased is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - PressedOn.TotalGameTime.TotalMilliseconds;
				KeyReleased(KeyCode, dt);
			}
		}
	}

	private GameTime PressedOn = new();
}
