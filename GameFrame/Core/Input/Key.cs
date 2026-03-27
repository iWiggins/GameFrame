using GameFrame.Core.Components;
using GameFrame.Core.EventHandlers;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Core.Input;
public class Key(Keyboard parent, Keys keycode) : Leaf(parent), IUpdate, IReset
{
	public Keys KeyCode { get; } = keycode;

	public bool IsDown { get; private set; } = false;

	public event KeyDownHandler? KeyPressed;
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
