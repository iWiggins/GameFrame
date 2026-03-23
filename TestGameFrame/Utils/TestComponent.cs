using GameFrame.Core.Components;

namespace TestGameFrame.Utils;
internal class TestComponent(int layer = 0) : Leaf(null, layer)
{
	public static List<TestComponent> CreateList(int size)
	{
		List<TestComponent> items = [];
		for(int i = 0; i < size; ++i) items.Add(new());
		return items;
	}
}
