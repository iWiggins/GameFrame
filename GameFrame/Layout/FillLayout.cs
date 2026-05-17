using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Layout;

/// <summary>
/// A layout that stretches all child components to fully fill its geometry.
/// </summary>
/// <param name="parent"><inheritdoc cref="Layout.Layout" path="/param[@name='parent']"/></param>
public class FillLayout(IComponent? parent = null) : Layout(parent), IMarginProvider
{

	public double Margins
	{
		set
		{
			Invalidate();
			_marginLeft = _marginRight = _marginTop = _marginBottom = value;
		}
	}

	public double MarginLeft
	{
		get => _marginLeft;
		set
		{
			Invalidate();
			_marginLeft = value;
		}
	}

	public double MarginRight
	{
		get => _marginRight;
		set
		{
			Invalidate();
			_marginRight = value;
		}
	}

	public double MarginTop
	{
		get => _marginTop;
		set
		{
			Invalidate();
			_marginTop = value;
		}
	}

	public double MarginBottom
	{
		get => _marginBottom;
		set
		{
			Invalidate();
			_marginBottom = value;
		}
	}

	protected override IEnumerable<IComponent> Arrange()
	{
		int topMargin = (int)(Height * MarginTop);
		int bottomMargin = (int)(Height * MarginBottom);
		int leftMargin = (int)(Width * MarginLeft);
		int rightMargin = (int)(Width * MarginRight);
		Rectangle geometry = new(
			x:X + leftMargin,
			y:Y + topMargin,
			width:Width - (leftMargin + rightMargin),
			height:Height - (topMargin + bottomMargin));
		foreach(var child in _children)
		{
			if(child is IGeometric geometric)
			{
				geometric.Geometry = geometry;
			}
		}
		return Order();
	}

	private double _marginLeft = 0;
	private double _marginRight = 0;
	private double _marginTop = 0;
	private double _marginBottom = 0;
}
