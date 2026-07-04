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
/// ABCs in the <see cref="GameFrame.Core.Geometrics"/> namespace ease development
/// of custom components by implementing the minimal boilerplate needed for geometric components.
/// </remarks>
/// <typeparam name="TChild">The type for this twig's child component.</typeparam>
/// <param name="child"><inheritdoc cref="Twig{TChild}.Twig" path="/param[@name='child']"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class GeometricTwig<TChild>(TChild child, IComponent? parent = null) : Twig<TChild>(child, parent), IGeometric where TChild : IComponent
{
	public Rectangle Geometry
	{
		get => Child is IGeometric geo ? geo.Geometry : _geometry;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Geometry = value;
			else _geometry = value;
		}
	}
	public Point Location
	{
		get => Geometry.Location;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Location = value;
			else _geometry.Location = value;
		}
	}
	public int X
	{
		get => Geometry.X;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.X = value;
			else _geometry.X = value;
		}
	}
	public int Y
	{
		get => Geometry.Y;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.X = value;
			else _geometry.X = value;
		}
	}
	public int Width
	{
		get => Geometry.Width;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Width = value;
			else _geometry.Width = value;
		}
	}
	public int Height
	{
		get => Geometry.Height;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Height = value;
			else _geometry.Height = value;
		}
	}
	public Point Center
	{
		get => Geometry.Center;
		set => SetCenter(value);
	}
	public int Left
	{
		get => Geometry.Left;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Left = value;
			else _geometry.X = value;
		}
	}
	public int Right
	{
		get => Geometry.Right;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Right = value;
			else _geometry.X = value - _geometry.Width;
		}
	}
	public int Top
	{
		get => Geometry.Top;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Top = value;
			else _geometry.Y = value;
		}
	}
	public int Bottom
	{
		get => Geometry.Bottom;
		set
		{
			Invalidate();
			if(Child is IGeometric geo) geo.Bottom = value;
			else _geometry.Y = value - _geometry.Height;
		}
	}

	public void SetCenter(Point p)
	{
		Invalidate();
		if(Child is IGeometric geo)
		{
			geo.SetCenter(p);
		}
		else
		{
			var loc = _geometry.MoveCenterpoint(p);
			_geometry.Location = loc;
		}
	}
	public void SetCenter(int x, int y)
	{
		Invalidate();
		if(Child is IGeometric geo)
		{
			geo.SetCenter(x, y);
		}
		else
		{
			var loc = _geometry.MoveCenterpoint(x, y);
			_geometry.Location = loc;
		}
	}
	public bool Overlaps(Point point) => Geometry.Contains(point);
	public bool Overlaps(int x, int y) => Geometry.Contains(x, y);

	private Rectangle _geometry;
}

