using GameFrame.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Layout;

/// <summary>
/// A layout that stretches components to fill its geometry while maintaining their relative dimentions.
/// </summary>
internal class ScaleLayout : Layout
{
	protected override IEnumerable<IComponent> Arrange()
	{
		foreach(var child in _children)
		{
			if(child is IGeometric geometric)
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

				geometric.Height = newHeight;
				geometric.Width = newWidth;

				geometric.SetCenter(Center);
			}
		}
		return Children.Order();
	}
}
