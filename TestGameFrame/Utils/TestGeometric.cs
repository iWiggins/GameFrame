using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace TestGameFrame.Utils;
internal class TestGeometric(ulong id, IComponent? parent = null, int layer = 0) : TestLeaf(id, parent, layer), IGeometric
{
	public Rectangle Geometry
	{
		get => _geometry;
		set => _geometry = value;
	}
	public int X { get => _geometry.X; set => _geometry.X = value; }
	public int Y { get => _geometry.Y; set => _geometry.Y = value; }
	public int Width { get =>_geometry.Width; set => _geometry.Width = value; }
	public int Height { get => _geometry.Height; set => _geometry.Height = value; }
	public Point Center { get => _geometry.Center; set => SetCenter(value); }
	public int Left
	{
		get => _geometry.Left;
		set => _geometry.X = value;
	}
	public int Right
	{
		get => _geometry.Right;
		set => _geometry.X = value - _geometry.Width;
	}
	public int Top
	{
		get => _geometry.Top;
		set => _geometry.Y = value;
	}
	public int Bottom
	{
		get => _geometry.Bottom;
		set => _geometry.Y = value - _geometry.Height;
	}

	public static List<TestGeometric> CreateList(int size, ulong firstId = 1, IComponent? parent = null, int layer = 0)
	{
		List<TestGeometric> items = [];
		ulong id = firstId;
		for(int i = 0; i < size; ++i) items.Add(new(id++, parent, layer));
		return items;
	}

	public bool Overlaps(Point point) => _geometry.Contains(point);
	public bool Overlaps(int x, int y) => _geometry.Contains(x, y);
	public void SetCenter(Point p) => SetCenter(p.X, p.Y);
	public void SetCenter(int x, int y)
	{
		_geometry.X = x - _geometry.Width / 2;
		_geometry.Y = y - _geometry.Height / 2;
	}

	private Rectangle _geometry = new();
}
