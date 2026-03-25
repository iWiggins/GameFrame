using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Layout;

/// <summary>
/// A layout that stretches all child components to fully fill its geometry.
/// </summary>
public class FillLayout(IComponent? parent = null) : Layout(parent)
{
	protected override IEnumerable<IComponent> Arrange()
	{
		foreach(var child in _children)
		{
			if(child is IGeometric geometric)
			{
				geometric.Geometry = _geometry;
			}
		}
		return Order();
	}
}
