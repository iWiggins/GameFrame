using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace GameFrame.Core.Clickables;

/// <summary>
/// Arguments for a clickable pressed event.
/// </summary>
/// <param name="button"><inheritdoc cref="Button" path="/summary"/></param>
/// <param name="position"><inheritdoc cref="Position" path="/summary"/></param>
public class ClickablePressedEventArgs(Mouse.Buttons button, Point position): EventArgs
{
	/// <summary>
	/// The button that was pressed.
	/// </summary>
	public Mouse.Buttons Button { get; } = button;
	/// <summary>
	/// The position when the button was pressed.
	/// </summary>
	public Point Position { get; } = position;
}
/// <summary>
/// Arguments for a mouse released event.
/// </summary>
/// <param name="button"></param>
/// <param name="position"></param>
/// <param name="button"><inheritdoc cref="Button" path="/summary"/></param>
/// <param name="position"><inheritdoc cref="Position" path="/summary"/></param>
public class ClickableReleasedEventArgs(Mouse.Buttons button, Point position, double duration) : EventArgs
{
	/// <summary>
	/// The button that was released.
	/// </summary>
	public Mouse.Buttons Button { get; } = button;
	/// <summary>
	/// The position when the button was released.
	/// </summary>
	public Point Position { get; } = position;
	/// <summary>
	/// The number of miliseconds the clickable was pressed.
	/// </summary>
	public double Duration { get; } = duration;
}

/// <summary>
/// Arguments for a mouse hovered event.
/// </summary>
/// <param name="position"><inheritdoc cref="Position" path="/summary"/></param>
public class ClickableHoveredEventArgs(Point position) : EventArgs
{
	/// <summary>
	/// The position of the mouse when the hover occurred.
	/// </summary>
	public Point Position { get; } = position;
}
/// <summary>
/// Arguments for a mouse unhovered event.
/// </summary>
/// <param name="position"><inheritdoc cref="Position" path="/summary"/></param>
/// <param name="duration"><inheritdoc cref="Duration" path="/summary"/></param>
public class ClickableUnhoveredEventArgs(Point position, double duration): EventArgs
{
	/// <summary>
	/// The position of the mouse when it left the clickable.
	/// </summary>
	public Point Position { get; } = position;
	/// <summary>
	/// How long the mouse was hovering.
	/// </summary>
	public double Duration { get; } = duration;
}
/// <summary>
/// Handler for a pressed event.
/// </summary>
public delegate void ClickablePressedHandler(IComponent sender, ClickablePressedEventArgs args);
/// <summary>
/// Handler for a released event.
/// </summary>
public delegate void ClickableReleasedHandler(IComponent sender, ClickableReleasedEventArgs args);
/// <summary>
/// Handler for a hovered event.
/// </summary>
public delegate void ClickableHoveredHandler(IComponent sender, ClickableHoveredEventArgs args);
/// <summary>
/// Handler for an unhovered event.
/// </summary>
public delegate void ClickableUnhoveredHandler(IComponent sender, ClickableUnhoveredEventArgs args);