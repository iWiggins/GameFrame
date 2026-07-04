/// Geometric versions of core component classes.
/// This code is generated and should not be manually edited.
using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace GameFrame.Core.Geometrics;

/// <summary>
/// A <see cref="Branch>"/> with geometry.
/// </summary>
/// <remarks>
/// ABCs in the <see cref="GameFrame.Core.Geometrics"/> namespace ease development
/// of custom components by implementing the minimal boilerplate needed for geometric components.
/// </remarks>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class GeometricBranch(IComponent? parent = null): Branch(parent), IGeometric
{
    public Rectangle Geometry
	{
		get => _geometry;
		set
		{
			Invalidate();
			_geometry = value;
		}
	}
	public int X
	{
		get => _geometry.X;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Y
	{
		get => _geometry.Y;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public Point Location
	{
		get => _geometry.Location;
		set
		{
			Invalidate();
			_geometry.Location = value;
		}
	}
	public int Width
	{
		get => _geometry.Width;
		set
		{
			Invalidate();
			_geometry.Width = value;
		}
	}
	public int Height
	{
		get => _geometry.Height;
		set
		{
			Invalidate();
			_geometry.Height = value;
		}
	}
	public Point Center
	{
		get => Geometry.Center;
		set => SetCenter(value);
	}
	public int Left
	{
		get => _geometry.Left;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Right
	{
		get => _geometry.Right;
		set
		{
			Invalidate();
			_geometry.X = value - _geometry.Width;
		}
	}
	public int Top
	{
		get => _geometry.Top;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public int Bottom
	{
		get => _geometry.Bottom;
		set
		{
			Invalidate();
			_geometry.Y = value - _geometry.Height;
		}
	}

	public void SetCenter(Point p)
	{
		Invalidate();

		var loc = _geometry.MoveCenterpoint(p);
		_geometry.Location = loc;
	}
	public void SetCenter(int x, int y)
	{
		Invalidate();

		var loc = _geometry.MoveCenterpoint(x, y);
		_geometry.Location = loc;
	}

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);

	private Rectangle _geometry;
}
/// <summary>
/// A <see cref="Component>"/> with geometry.
/// </summary>
/// <remarks>
/// ABCs in the <see cref="GameFrame.Core.Geometrics"/> namespace ease development
/// of custom components by implementing the minimal boilerplate needed for geometric components.
/// </remarks>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class GeometricComponent(IComponent? parent = null): Component(parent), IGeometric
{
    public Rectangle Geometry
	{
		get => _geometry;
		set
		{
			Invalidate();
			_geometry = value;
		}
	}
	public int X
	{
		get => _geometry.X;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Y
	{
		get => _geometry.Y;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public Point Location
	{
		get => _geometry.Location;
		set
		{
			Invalidate();
			_geometry.Location = value;
		}
	}
	public int Width
	{
		get => _geometry.Width;
		set
		{
			Invalidate();
			_geometry.Width = value;
		}
	}
	public int Height
	{
		get => _geometry.Height;
		set
		{
			Invalidate();
			_geometry.Height = value;
		}
	}
	public Point Center
	{
		get => Geometry.Center;
		set => SetCenter(value);
	}
	public int Left
	{
		get => _geometry.Left;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Right
	{
		get => _geometry.Right;
		set
		{
			Invalidate();
			_geometry.X = value - _geometry.Width;
		}
	}
	public int Top
	{
		get => _geometry.Top;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public int Bottom
	{
		get => _geometry.Bottom;
		set
		{
			Invalidate();
			_geometry.Y = value - _geometry.Height;
		}
	}

	public void SetCenter(Point p)
	{
		Invalidate();

		var loc = _geometry.MoveCenterpoint(p);
		_geometry.Location = loc;
	}
	public void SetCenter(int x, int y)
	{
		Invalidate();

		var loc = _geometry.MoveCenterpoint(x, y);
		_geometry.Location = loc;
	}

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);

	private Rectangle _geometry;
}
/// <summary>
/// A <see cref="Leaf>"/> with geometry.
/// </summary>
/// <remarks>
/// ABCs in the <see cref="GameFrame.Core.Geometrics"/> namespace ease development
/// of custom components by implementing the minimal boilerplate needed for geometric components.
/// </remarks>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class GeometricLeaf(IComponent? parent = null): Leaf(parent), IGeometric
{
    public Rectangle Geometry
	{
		get => _geometry;
		set
		{
			Invalidate();
			_geometry = value;
		}
	}
	public int X
	{
		get => _geometry.X;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Y
	{
		get => _geometry.Y;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public Point Location
	{
		get => _geometry.Location;
		set
		{
			Invalidate();
			_geometry.Location = value;
		}
	}
	public int Width
	{
		get => _geometry.Width;
		set
		{
			Invalidate();
			_geometry.Width = value;
		}
	}
	public int Height
	{
		get => _geometry.Height;
		set
		{
			Invalidate();
			_geometry.Height = value;
		}
	}
	public Point Center
	{
		get => Geometry.Center;
		set => SetCenter(value);
	}
	public int Left
	{
		get => _geometry.Left;
		set
		{
			Invalidate();
			_geometry.X = value;
		}
	}
	public int Right
	{
		get => _geometry.Right;
		set
		{
			Invalidate();
			_geometry.X = value - _geometry.Width;
		}
	}
	public int Top
	{
		get => _geometry.Top;
		set
		{
			Invalidate();
			_geometry.Y = value;
		}
	}
	public int Bottom
	{
		get => _geometry.Bottom;
		set
		{
			Invalidate();
			_geometry.Y = value - _geometry.Height;
		}
	}

	public void SetCenter(Point p)
	{
		Invalidate();

		var loc = _geometry.MoveCenterpoint(p);
		_geometry.Location = loc;
	}
	public void SetCenter(int x, int y)
	{
		Invalidate();

		var loc = _geometry.MoveCenterpoint(x, y);
		_geometry.Location = loc;
	}

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);

	private Rectangle _geometry;
}
