using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Layout;

/// <summary>
/// A layout that arranges children in relative positioning.
/// When resized, the children maintain their relative positions and relative sizes.
/// </summary>
/// <param name="parent"></param>
/// <param name="width"></param>
/// <param name="height"></param>
public class CanvasLayout(IComponent? parent = null) : Layout(parent)
{

	protected override IEnumerable<IComponent> Arrange()
	{
		if(_lastGeometry is not null && _children.Count > 0)
		{
			double xScale = Width / _lastGeometry.Value.Width;
			double yScale = Height / _lastGeometry.Value.Height;

			foreach(var child in _children)
			{
				if(child is IGeometric geometric)
				{
					int dispX = geometric.X - _lastGeometry.Value.X;
					int dispY = geometric.Y - _lastGeometry.Value.Y;
					int relX = (int)(dispX * xScale);
					int relY = (int)(dispY * yScale);
					int x = X + relX;
					int y = Y + relY;
					int width = (int)(geometric.Width * xScale);
					int height = (int)(geometric.Height * yScale);
					geometric.Geometry = new(x, y, width, height);
				}
			}
		}

		_lastGeometry = Geometry;

		return Order();
	}

	private Rectangle? _lastGeometry;
}
