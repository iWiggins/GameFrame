using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mouse = GameFrame.Core.Input.Mouse;

namespace GameFrame.Core.EventHandlers;

public delegate void MouseDownHandler(Mouse.Buttons button, Point position);
public delegate void MouseUpHandler(Mouse.Buttons button, Point position, double duration);
public delegate void MouseHoverHandler();
public delegate void MouseUnhoverHandler(double duration);

public delegate void KeyDownHandler(Keys key);
public delegate void KeyUpHandler(Keys key, double duration);
