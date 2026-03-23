using GameFrame.Core.Geometrics;

namespace TestGameFrame.Utils;
internal class TestGeometric(int layer = 0) : GeometricLeaf(null, layer)
{
	public static List<TestGeometric> CreateList(int size)
	{
		List<TestGeometric> items = [];
		for(int i = 0; i < size; ++i) items.Add(new());
		return items;
	}
}
