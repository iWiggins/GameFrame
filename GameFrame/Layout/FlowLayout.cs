using GameFrame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Layout;

/// <summary>
/// Arranges components in a linear method that fills the component by proportions.
/// </summary>
/// <param name="direction"></param>
public class FlowLayout(FlowLayout.Direction direction) : MetaLayout<int>
{
	public enum Direction
	{
		Left,
		Right,
		Down,
		Up
	}

	public bool AddChild(IComponent component, int proportion)
	{
		Invalidate();

		if(_children.Add(component))
		{
			AddEntry(component, component is IGeometric ? proportion : 0); // TODO: Document this behavior on both ADD functions
			return true;
		}
		else
		{
			return false;
		}
	}

	public override bool AddChild(IComponent component) => AddChild(component, 1);

	public bool AddFiller(int proportion = 1) => AddChild(new Filler(), proportion);

	/// <summary>
	/// Removes the filler based on order they were added.
	/// </summary>
	/// <param name="index">The index of the filler to remove. If greater than the number of fillers, removes last.</param>
	/// <returns>If a filler was removed.</returns>
	public bool RemoveFiller(int index = 0)
	{
		Invalidate();

		IComponent? filler = null;
		foreach(var child in _children)
		{
			if(child is Filler)
			{
				filler = child;
				if(index == 0) break;
				else --index;
			}
		}
		if(filler is not null)
		{
			RemoveChild(filler);
			return true;
		}
		else
		{
			return false;
		}
	}

	protected override IEnumerable<IComponent> Arrange()
	{
		int totalProportions = Entries.Sum();
		int unitProportion = totalProportions / _children.Count;

		int offset = 0;

		void flowLeft(IGeometric geometric, int proportion)
		{
			geometric.Height = Height;
			geometric.Width = unitProportion * proportion;
			geometric.Y = Y;
			geometric.X = offset;
			offset += unitProportion;
		}
		void flowRight(IGeometric geometric, int proportion)
		{
			geometric.Height = Height;
			geometric.Width = unitProportion * proportion;
			geometric.Y = Y;
			geometric.X = X + Width - offset;
			offset += unitProportion;
		}
		void flowDown(IGeometric geometric, int proportion)
		{
			geometric.Width = Width;
			geometric.Height = unitProportion * proportion;
			geometric.X = X;
			geometric.Y = offset;
			offset += unitProportion;
		}
		void flowUp(IGeometric geometric, int proportion)
		{
			geometric.Width = Width;
			geometric.Height = unitProportion * proportion;
			geometric.X = X;
			geometric.Y = Y + Height - offset;
		}

		foreach(var component in _children)
		{
			if(component is IGeometric geometric)
			{
				int proportion = GetEntry(component);

				switch(direction)
				{
					case Direction.Left:
						flowLeft(geometric, proportion);
						break;
					case Direction.Right:
						flowRight(geometric, proportion);
						break;
					case Direction.Down:
						flowDown(geometric, proportion);
						break;
					case Direction.Up:
						flowUp(geometric, proportion);
						break;
					default:
						throw new InvalidOperationException("Invalid FlowLayout direction.");
				}
			}
		}

		return Order();
	}

	protected override IEnumerable<IComponent> Order()
	{
		// modification to the Layout order function to skip fillers
		return _cache =
			_children
			.Where(c => c is not Filler)
			.OrderBy(c => c.Layer)
			.ThenBy(c => c.Id)
			.ToArray();
	}
}
