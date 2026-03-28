using Microsoft.Xna.Framework;
using Mouse = GameFrame.Core.Input.Mouse;

namespace GameFrame.Core.EventHandlers;

/// <summary>
/// Handler for a mouse down event.
/// </summary>
/// <remarks>
/// Used by <see cref="Mouse"/> and in <see cref="GameFrame.Core.Clickables"/>.
/// </remarks>
/// <param name="button">Which mouse button was pressed.</param>
/// <param name="position">The position of the mouse.</param>
public delegate void MouseDownHandler(Mouse.Buttons button, Point position);

/// <summary>
/// Handler for a mouse up event.
/// </summary>
/// /// <remarks>
/// Used by <see cref="Mouse"/> and in <see cref="GameFrame.Core.Clickables"/>.
/// </remarks>
/// <param name="button">Which mouse button was released.</param>
/// <param name="position">The position of the mouse.</param>
/// <param name="duration">How long the button was held for.</param>
public delegate void MouseUpHandler(Mouse.Buttons button, Point position, double duration);

/// <summary>
/// Handler for a mouse hover event.
/// </summary>
/// <remarks>
/// Used in <see cref="GameFrame.Core.Clickables"/>.
/// </remarks>
public delegate void MouseHoverHandler();
/// <summary>
/// Handler for a mouse hover end event.
/// </summary>
/// <remarks>
/// Used in <see cref="GameFrame.Core.Clickables"/>.
/// </remarks>
/// <param name="duration">How long the mouse was hovering before it was removed.</param>
public delegate void MouseUnhoverHandler(double duration);
