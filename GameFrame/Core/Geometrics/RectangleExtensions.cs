using Microsoft.Xna.Framework;

namespace GameFrame.Core.Geometrics;

/// <summary>
/// Extension functions for common Rectangle math.
/// </summary>
internal static class RectangleExtensions
{
	/// <summary>
	/// Returns a new point representing the position of the rectangle after its center has been moved.
	/// </summary>
	/// <remarks>
	/// This function does not mutate the rectangle, it creates a new point.
	/// </remarks>
	/// <param name="rect">The rectangle used to calculate the new center.</param>
	/// <param name="x">The x position of the new center.</param>
	/// <param name="y">The y position of the new center.</param>
	/// <returns>A new point representing where the location of the rectangle centered on (<paramref name="x"/>,<paramref name="y"/>) would be.</returns>
	public static Point MoveCenterpoint(this Rectangle rect, int x, int y) =>
		new(
			x - rect.Width / 2,
			y - rect.Height / 2
			);

	/// <summary>
	/// <inheritdoc cref="MoveCenterpoint(Rectangle, int, int)" path="/summary"/>
	/// </summary>
	/// <remarks>
	/// <inheritdoc cref="MoveCenterpoint(Rectangle, int, int)" path="/remarks"/>
	/// </remarks>
	/// <param name="rect">The rectangle used to calculate the new center.</param>
	/// <param name="center">The position of the new center.</param>
	/// <returns>A new point representing where the location of the rectangle centered on <paramref name="center"/> would be.</returns>
	public static Point MoveCenterpoint(this Rectangle rect, Point center) =>
		MoveCenterpoint(rect, center.X, center.Y);
}
