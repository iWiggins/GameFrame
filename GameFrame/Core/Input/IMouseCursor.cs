using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Core.Input;
/// <summary>
/// A component which represents a visible mouse cursor on screen.
/// </summary>
public interface IMouseCursor : IComponent, IDraw
{
	/// <summary>
	/// Whether the cursor image is centered on the real mouse position.
	/// </summary>
	/// <remarks>
	/// Most default cursors, like the Windows arrow, are not centered.
	/// The arrow points to the real position.
	/// </remarks>
	bool Centered { get; set; }

	/// <summary>
	/// The color of the mouse cursor.
	/// </summary>
	Color Color { get; set; }

	/// <summary>
	/// The real position of the mouse cursor.
	/// </summary>
	Point Position { get; }

	/// <summary>
	/// The visible texture of the mouse cursor.
	/// </summary>
	Texture2D Texture { get; set; }

	/// <summary>
	/// Moves the cursor to the specified location.
	/// </summary>
	/// <remarks>
	/// Implementers should take care to consider the effects of <see cref="Centered"/>.
	/// </remarks>
	/// <param name="x">The X coordinate on screen.</param>
	/// <param name="y">The y coordinate on screen.</param>
	void Move(int x, int y);

	/// <summary>
	/// <inheritdoc cref="Move(int, int)" path="/summary"/>
	/// </summary>
	/// <remarks>
	/// <inheritdoc cref="Move(int, int)" path="/remarks"/>
	/// </remarks>
	/// <param name="p">The on screen position to move to.</param>
	void Move(Point p);

	/// <summary>
	/// <inheritdoc cref="Move(int, int)" path="/summary"/>
	/// </summary>
	/// <remarks>
	/// <inheritdoc cref="Move(int, int)" path="/remarks"/>
	/// </remarks>
	/// <param name="p">The on screen position to move to.</param>
	void Move(Vector2 p);
}