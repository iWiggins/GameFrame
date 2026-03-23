/// Geometric versions of core component classes.
/// This code is generated and should not be manually edited.
using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace GameFrame.Core.Geometrics;

/// <summary>
/// A <see cref="Branch>"/> with geometry.
/// </summary>
public abstract class GeometricBranch: Branch, IGeometric
{
    public Rectangle Geometry
	{
		get => _geometry;
		set
		{
			_geometry = value;
			Invalidate();
		}
	}
	public int X
	{
		get => _geometry.X;
		set
		{
			_geometry.X = value;
			Invalidate();
		}
	}
	public int Y
	{
		get => _geometry.Y;
		set
		{
			_geometry.Y = value;
			Invalidate();
		}
	}
	public int Width
	{
		get => _geometry.Width;
		set
		{
			_geometry.Width = value;
			Invalidate();
		}
	}
	public int Height
	{
		get => _geometry.Height;
		set
		{
			_geometry.Height = value;
			Invalidate();
		}
	}
	public Point Center
	{
		get => _geometry.Center;
		set => _geometry.SetCenter(value);
	}

	protected GeometricBranch(IComponent? parent = null, int layer = 0):
	base(parent, layer)
	{ }

	public void SetCenter(Point p) => _geometry.SetCenter(p);
	public void SetCenter(int x, int y) => _geometry.SetCenter(x, y);

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);

	private Rectangle _geometry;
}
/// <summary>
/// A <see cref="Component>"/> with geometry.
/// </summary>
public abstract class GeometricComponent: Component, IGeometric
{
    public Rectangle Geometry
	{
		get => _geometry;
		set
		{
			_geometry = value;
			Invalidate();
		}
	}
	public int X
	{
		get => _geometry.X;
		set
		{
			_geometry.X = value;
			Invalidate();
		}
	}
	public int Y
	{
		get => _geometry.Y;
		set
		{
			_geometry.Y = value;
			Invalidate();
		}
	}
	public int Width
	{
		get => _geometry.Width;
		set
		{
			_geometry.Width = value;
			Invalidate();
		}
	}
	public int Height
	{
		get => _geometry.Height;
		set
		{
			_geometry.Height = value;
			Invalidate();
		}
	}
	public Point Center
	{
		get => _geometry.Center;
		set => _geometry.SetCenter(value);
	}

	protected GeometricComponent(IComponent? parent = null, int layer = 0):
	base(parent, layer)
	{ }

	public void SetCenter(Point p) => _geometry.SetCenter(p);
	public void SetCenter(int x, int y) => _geometry.SetCenter(x, y);

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);

	private Rectangle _geometry;
}
/// <summary>
/// A <see cref="Leaf>"/> with geometry.
/// </summary>
public abstract class GeometricLeaf: Leaf, IGeometric
{
    public Rectangle Geometry
	{
		get => _geometry;
		set
		{
			_geometry = value;
			Invalidate();
		}
	}
	public int X
	{
		get => _geometry.X;
		set
		{
			_geometry.X = value;
			Invalidate();
		}
	}
	public int Y
	{
		get => _geometry.Y;
		set
		{
			_geometry.Y = value;
			Invalidate();
		}
	}
	public int Width
	{
		get => _geometry.Width;
		set
		{
			_geometry.Width = value;
			Invalidate();
		}
	}
	public int Height
	{
		get => _geometry.Height;
		set
		{
			_geometry.Height = value;
			Invalidate();
		}
	}
	public Point Center
	{
		get => _geometry.Center;
		set => _geometry.SetCenter(value);
	}

	protected GeometricLeaf(IComponent? parent = null, int layer = 0):
	base(parent, layer)
	{ }

	public void SetCenter(Point p) => _geometry.SetCenter(p);
	public void SetCenter(int x, int y) => _geometry.SetCenter(x, y);

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);

	private Rectangle _geometry;
}
