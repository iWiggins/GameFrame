using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameFrame.Layout;

/// <summary>
/// A layout that stretches components to fill its geometry while maintaining their relative dimentions.
/// </summary>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class ScaleLayout(IComponent? parent = null) : Layout(parent), IMarginProvider
{
	public double Margins { set => _leftMargin = _rightMargin = _topMargin = _bottomMargin = value; }
	public double MarginLeft
	{
		get => _leftMargin;
		set => _leftMargin = value;
	}
	public double MarginRight
	{
		get => _rightMargin;
		set => _rightMargin = value;
	}
	public double MarginTop
	{
		get => _topMargin;
		set => _topMargin = value;
	}
	public double MarginBottom
	{
		get => _bottomMargin;
		set => _bottomMargin = value;
	}
	protected override IEnumerable<IComponent> Arrange()
	{
		int leftMargin = (int)(Width * _leftMargin);
		int rightMargin = (int)(Width * _rightMargin);
		int topMargin = (int)(Height * _topMargin);
		int bottomMargin = (int)(Height * _bottomMargin);
		Rectangle geometry = new(
			x: X + leftMargin,
			y: Y + topMargin,
			width: Width - (leftMargin + rightMargin),
			height: Height - (topMargin + bottomMargin));
		foreach(var child in _children)
		{
			if(child is IGeometric geometric)
			{
				// If the child's width or height are 0,
				// revert to a fill instead of a scale.
				if(geometric.Width == 0 || geometric.Height == 0)
				{
					geometric.Geometry = geometry;
				}
				else
				{
					// by default, attempt to scale height first
					double newHeight = geometry.Height;
					double scaleFactor = newHeight / geometric.Height;
					double newWidth = scaleFactor * geometric.Width;

					// If new width would exceed parent, set new width to parent
					// width and calculate new scale factor for the height
					if(newWidth > geometry.Width)
					{
						newWidth = geometry.Width;
						scaleFactor = newWidth / geometric.Width;
						newHeight = scaleFactor * geometric.Height;
					}

					// Create a new rectangle to avoid multiple calls to invalidate on the child.
					Rectangle newGeometry = new()
					{
						Height = (int)newHeight,
						Width = (int)newWidth,

						X = Center.X - (int)newWidth / 2,
						Y = Center.Y - (int)newHeight / 2
					};

					geometric.Geometry = newGeometry;
				}
			}
		}
		return _cache = Order();
	}

	private double _leftMargin, _rightMargin, _topMargin, _bottomMargin;
}
