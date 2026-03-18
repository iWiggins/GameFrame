using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Mouse = GameFrame.Core.Input.Mouse;

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace GameFrame.Core.EventHandlers;
#pragma warning restore IDE0130 // Namespace does not match folder structure

public delegate bool MouseDownHandler(Mouse.Buttons button, Point position);
public delegate bool MouseUpHandler(Mouse.Buttons button, Point position, double duration);
public delegate bool MouseHoverHandler();
public delegate bool MouseUnhoverHandler(double duration);

public delegate bool KeyDownHandler(Keys key);
public delegate bool KeyUpHandler(Keys key, double duration);
