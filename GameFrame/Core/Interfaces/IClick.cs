using Microsoft.Xna.Framework;
using GameFrame.Core.Input;
using GameFrame.Core.Clickables;

namespace GameFrame.Core.Interfaces;
/// <summary>
/// A component that interacts with mouse pointers and clicks.
/// </summary>
public interface IClick: IReset
{
	/// <summary>
	/// An event raised when the component is pressed.
	/// </summary>
	event ClickablePressedHandler? Pressed;
	/// <summary>
	/// An event raised when the component is released.
	/// </summary>
	event ClickableReleasedHandler? Released;
	/// <summary>
	/// An event raised when the component is hovered over with a pointer.
	/// </summary>
	event ClickableHoveredHandler? Hovered;
	/// <summary>
	/// An event raised when a pointer leaves the component.
	/// </summary>
	event ClickableUnhoveredHandler? Unhovered;
	/// <summary>
	/// Whether the component is currently held down.
	/// </summary>
	bool Down { get; }
	/// <summary>
	/// The button held down.
	/// </summary>
	Mouse.Buttons? DownButton { get; }
	/// <summary>
	/// Whether a pointer is currently hovering over the component.
	/// </summary>
	bool Hovering { get; }
	/// <summary>
	/// Checks if the component has been hovered or clicked,
	/// sets states, and raises events.
	/// </summary>
	/// <param name="time">The time since the last update.</param>
	/// <param name="mouse">A reference to the mouse.</param>
	void Click(GameTime time, Mouse mouse);
}
