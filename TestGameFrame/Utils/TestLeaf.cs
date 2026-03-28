using GameFrame.Core.Interfaces;

namespace TestGameFrame.Utils;
internal class TestLeaf(ulong id, IComponent? parent = null, int layer = 0) : IComponent
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

	public IEnumerable<IComponent> Children => [];

	public bool HasChildren => false;

	public bool AddChild(IComponent component) => false;

	public void Invalidate()
	{ }
	public bool RemoveChild(IComponent component) => false;


	public static List<TestLeaf> CreateList(int size, ulong firstId = 1, IComponent? parent = null)
	{
		List<TestLeaf> items = [];
		ulong id = firstId;
		for(int i = 0; i < size; ++i) items.Add(new(id++, parent));
		return items;
	}


	private int _layer = layer;
}
