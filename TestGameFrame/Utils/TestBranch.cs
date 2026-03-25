using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;

namespace TestGameFrame.Utils;
internal class TestBranch(ulong id, IComponent? parent = null, int layer = 0) : IComponent
{
	public IComponent? Parent => parent;

	public ulong Id => id;

	public int Layer
	{
		get => _layer;
		set
		{
			_layer = value;
			parent?.Invalidate();
		}
	}
	public bool Enabled { get; set; } = true;

	public IEnumerable<IComponent> Children => _children;

	public bool HasChildren => _children.Count > 0;

	public bool AddChild(IComponent component) => _children.Add(component);

	public void Invalidate()
	{
		foreach(var child in Children)
		{
			child.Invalidate();
		}
	}
	public bool RemoveChild(IComponent component) => _children.Remove(component);


	public static List<TestBranch> CreateList(int size, ulong firstId = 1, IComponent? parent = null)
	{
		List<TestBranch> items = [];
		ulong id = firstId;
		for(int i = 0; i < size; ++i) items.Add(new(id++, parent));
		return items;
	}


	private int _layer = layer;
	private readonly HashSet<IComponent> _children = [];
}
