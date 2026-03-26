using Microsoft.Xna.Framework;

namespace GameFrame.Core.Interfaces;
public interface IGeometric
{
	/// <summary>
	/// The bounding box for the component.
	/// </summary>
	Rectangle Geometry { get; set; }

	/// <summary>
	/// The X coordinate of the component.
	/// </summary>
	int X { get; set; }

	/// <summary>
	/// The Y coordinate of the component.
	/// </summary>
	int Y { get; set; }

	/// <summary>
	/// The component's width.
	/// </summary>
	int Width { get; set; }

	/// <summary>
	/// The component's height.
	/// </summary>
	int Height { get; set; }

	/// <summary>
	/// The center point of the component.
	/// </summary>
	Point Center { get; set; }

	/// <summary>
	/// The x coordinate of the left side of the component.
	/// </summary>
	int Left { get; set; }

	/// <summary>
	/// The x coordinate of the right side of the component.
	/// </summary>
	int Right { get; set; }

	/// <summary>
	/// The y coordinate of the top of the component.
	/// </summary>
	int Top { get; set; }

	/// <summary>
	/// The y coordinate of the bottom of the component.
	/// </summary>
	int Bottom { get; set; }

	/// <summary>
	/// Sets the center of the component.
	/// </summary>
	/// <param name="p">New position of the center.</param>
	void SetCenter(Point p);

	/// <summary>
	/// Sets the center of the component.
	/// </summary>
	/// <param name="x">X position of the center.</param>
	/// <param name="y">Y position of the center.</param>
	void SetCenter(int x, int y);

	/// <summary>
	/// Whether the component overlaps a point.
	/// </summary>
	/// <param name="point">The <see cref="Point"/> to check.</param>
	/// <returns>If the <paramref name="point"/> is inside the component's <see cref="Geometry"/>.</returns>
	bool Overlaps(Point point);

	/// <summary>
	/// <inheritdoc cref="Overlaps(Point)" path="/summary"/>
	/// </summary>
	/// <param name="x">The X coordinate of the point to check.</param>
	/// <param name="y">The Y coordinate of the point to check.</param>
	/// <returns>If the coordinates are inside the component's <see cref="Geometry"/>.</returns>
	bool Overlaps(int x, int y);
}
