using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Layout;

public abstract class Layout: IComponent, IGeometric
{
    public IComponent? Parent { get; }

    public ulong Id { get; }

    public int Layer { get; set; }
    public bool Enabled { get; set; }

    public IEnumerable<IComponent> Children =>
    _cache is not null ? _cache : _cache = Arrange();

    public bool HasChildren => _children.Count > 0;

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
        set
        {
            SetCenter(value.X, value.Y);
            Invalidate();
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
        _geometry.SetCenter(p);
        Invalidate();
	}

	public virtual void SetCenter(int x, int y)
    {
		_geometry.SetCenter(x, y);
        Invalidate();
	}

    public bool Overlaps(Point point) =>
        _geometry.Contains(point);

    public bool Overlaps(int x, int y) =>
        _geometry.Contains(x, y);

    /// <summary>
    /// Forces an arrangement to occur, rather than doing so lazily.
    /// </summary>
    public void ArrangeChildren() => Arrange();

    protected abstract IEnumerable<IComponent> Arrange();
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