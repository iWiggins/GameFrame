using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Layout;

/// <summary>
/// A layout that stretches components to fill its geometry while maintaining their relative dimentions.
/// </summary>
public class ScaleLayout : Layout
{
	protected override IEnumerable<IComponent> Arrange()
	{
		foreach(var child in _children)
		{
			if(child is IGeometric geometric)
			{
				// If the child's width or height are 0,
				// revert to a fill instead of a scale.
				if(geometric.Width == 0 || geometric.Height == 0)
				{
					geometric.Geometry = _geometry;
				}
				else
				{
					// by default, attempt to scale height first
					int newHeight = Height;
					double scaleFactor = newHeight / geometric.Height;
					int newWidth = (int)(scaleFactor * geometric.Width);

					// If new width would exceed parent, set new width to parent
					// width and calculate new scale factor for the height
					if(newWidth > Width)
					{
						newWidth = Width;
						scaleFactor = newWidth / geometric.Width;
						newHeight = (int)(scaleFactor * geometric.Height);
					}

					// Create a new rectangle to avoid multiple calls to invalidate on the child.
					Rectangle newGeometry = geometric.Geometry;

					newGeometry.Height = newHeight;
					newGeometry.Width = newWidth;

					newGeometry.X = Center.X - newWidth / 2;
					newGeometry.Y = Center.Y - newHeight / 2;

					geometric.Geometry = newGeometry;
				}
			}
		}
		return _cache = Order();
	}
}
