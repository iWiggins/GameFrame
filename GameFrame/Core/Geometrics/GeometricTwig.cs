using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace GameFrame.Core.Geometrics;
/// <summary>
/// A <see cref="Twig{TChild}"/> with geometry.
/// If the child element is geometric, this component shares the child's geometry.
/// If the child element is not geometric, this component generates its own geometry.
/// </summary>
/// <remarks>
/// Because Child is readonly, the JIT should optimize all of the if/else logic when
/// the component is first accessed, leading to 0 overhead for the child geometry check.
/// </remarks>
/// <typeparam name="TChild">The type for this twig's child component.</typeparam>
public abstract class GeometricTwig<TChild> : Twig<TChild>, IGeometric where TChild : IComponent
{
	protected GeometricTwig(TChild child, IComponent? parent = null, int layer = 0):
		base(child, parent, layer)
	{

	}

	public Rectangle Geometry
	{
		get => Child is IGeometric geo ? geo.Geometry : _geometry;
		set
		{
			if(Child is IGeometric geo) geo.Geometry = value;
			else _geometry = value;
			Parent?.Invalidate();
		}
	}
	public int X
	{
		get => Geometry.X;
		set
		{
			if(Child is IGeometric geo) geo.X = value;
			else _geometry.X = value;
			Parent?.Invalidate();
		}
	}
	public int Y
	{
		get => Geometry.Y;
		set
		{
			if(Child is IGeometric geo) geo.X = value;
			else _geometry.X = value;
			Parent?.Invalidate();
		}
	}
	public int Width
	{
		get => Geometry.Width;
		set
		{
			if(Child is IGeometric geo) geo.Width = value;
			else _geometry.Width = value;
			Parent?.Invalidate();
		}
	}
	public int Height
	{
		get => Geometry.Height;
		set
		{
			if(Child is IGeometric geo) geo.Height = value;
			else _geometry.Height = value;
			Parent?.Invalidate();
		}
	}
	public Point Center
	{
		get => Geometry.Center;
		set => Geometry.SetCenter(value);
	}

	public void SetCenter(Point p) => _geometry.SetCenter(p);
	public void SetCenter(int x, int y) => _geometry.SetCenter(x, y);

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);

	private Rectangle _geometry;
}

