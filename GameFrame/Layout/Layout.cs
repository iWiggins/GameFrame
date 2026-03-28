using GameFrame.Core;
using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Layout;

/// <summary>
/// Base class for all Layout classes.
/// A layout is a UI element that spatially organizes its children in a configurable manner.
/// </summary>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class Layout(IComponent? parent) : IComponent, IGeometric, IInitialize
{
	public IComponent? Parent { get; } = parent;

	public ulong Id { get; } = Identity.GenerateId();

	public int Layer { get; set; } = 0;
	public bool Enabled { get; set; } = true;

    public bool Initialized { get; private set; }

	public IEnumerable<IComponent> Children =>
    _cache is not null ? _cache : _cache = Arrange();

    public bool HasChildren => _children.Count > 0;

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
        get => _geometry.Center;
        set
        {
            Invalidate();
			SetCenter(value);
        }
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

	public virtual bool AddChild(IComponent component)
    {
        if(_children.Add(component))
        {
            Invalidate();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Invalidate()
    {
        _cache = null;
        foreach(var child in _children)
        {
            child.Invalidate();
        }
    }

    public virtual bool RemoveChild(IComponent component)
    {
        if(_children.Remove(component))
        {
            Invalidate();
            return true;
        }
        else
        {
            return false;
        }
    }

	public virtual void SetCenter(Point p)
	{
        Invalidate();
        var loc = _geometry.MoveCenterpoint(p);
        _geometry.Location = loc;
	}

	public virtual void SetCenter(int x, int y)
    {
        Invalidate();
		var loc = _geometry.MoveCenterpoint(x, y);
        _geometry.Location = loc;
	}

    public bool Overlaps(Point point) =>
        _geometry.Contains(point);

    public bool Overlaps(int x, int y) =>
        _geometry.Contains(x, y);

    public void Initialize()
    {
		Arrange();
        Initialized = true;
	}

	public bool Contains(IComponent component) =>
        _children.Contains(component);

    /// <summary>
    /// Forces an arrangement to occur, rather than doing so lazily.
    /// </summary>
    public void ArrangeChildren() => Arrange();

    /// <summary>
    /// Performs the logic of spatially organizing all child components.
    /// </summary>
    /// <returns>The children in draw order.</returns>
    protected abstract IEnumerable<IComponent> Arrange();

    /// <summary>
    /// Performs the logic of ordering all child components.
    /// By default, orders according to layer, then ID.
    /// </summary>
    /// <returns>The children in draw order.</returns>
    protected virtual IEnumerable<IComponent> Order()
    {
		return _cache =
			_children
			.OrderBy(c => c.Layer)
			.ThenBy(c => c.Id)
			.ToArray();
	}


	protected Rectangle _geometry;
    protected readonly HashSet<IComponent> _children = [];
    protected IEnumerable<IComponent>? _cache = null;
}