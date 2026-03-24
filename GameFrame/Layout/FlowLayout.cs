using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Layout;

/// <summary>
/// Arranges components in a linear method that fills the component by proportions.
/// </summary>
/// <param name="direction"></param>
public class FlowLayout(FlowLayout.Direction direction) : MetaLayout<FlowLayout.Metadata>
{
	public enum Direction
	{
		Right,
		Left,
		Down,
		Up
	}

	public record Metadata(int Proportion, int Order);

	public bool AddChild(IComponent component, int proportion)
	{
		Invalidate();

		if(_children.Add(component))
		{
			if(component is not IGeometric) proportion = 0;// TODO: Document this behavior on both ADD functions
			AddEntry(component, new(proportion, count++)); 
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
		if(_children.Count != 0)
		{
			var children = (
				from child in _children
				where child is IGeometric
				orderby GetEntry(child).Order
				select new { Child = child as IGeometric, GetEntry(child).Proportion })
				.ToArray();

			int totalProportions = Entries.Sum(e => e.Proportion);
			int unitWidth = direction switch
			{
				Direction.Right => Width / totalProportions,
				Direction.Left => Width / totalProportions,
				Direction.Down => Height / totalProportions,
				Direction.Up => Height / totalProportions,
				_ => throw new InvalidOperationException("Invalid FlowLayout direction."),
			};

			int offset = 0;

			void flowRight(IGeometric geometric, int proportion)
			{
				int width = proportion * unitWidth;
				geometric.Geometry = new(
					x: X + offset,
					y: Y,
					width: width,
					height: Height);
				offset += width;
			}
			void flowLeft(IGeometric geometric, int proportion)
			{
				int width = proportion * unitWidth;
				geometric.Geometry = new(
					x: X + Width - offset - width,
					y: Y,
					width: width,
					height: Height);
				offset += width;
			}
			void flowDown(IGeometric geometric, int proportion)
			{
				int height = proportion * unitWidth;
				geometric.Geometry = new(
					x: X,
					y: Y + offset,
					width: Width,
					height: height);
				offset += height;
			}
			void flowUp(IGeometric geometric, int proportion)
			{
				int height = proportion * unitWidth;
				geometric.Geometry = new(
					x: X,
					y: Y  + Height - offset - height,
					width: Width,
					height: height);
				offset += height;
			}

			foreach(var component in children)
			{
				switch(direction)
				{
					case Direction.Right:
						flowRight(component.Child, component.Proportion);
						break;
					case Direction.Left:
						flowLeft(component.Child, component.Proportion);
						break;
					case Direction.Down:
						flowDown(component.Child, component.Proportion);
						break;
					case Direction.Up:
						flowUp(component.Child, component.Proportion);
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

	private int count = 0;
}
