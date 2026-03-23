using GameFrame.Layout;
using TestGameFrame.Utils;

namespace TestGameFrame.Layout;
public abstract class TestLayout(GameFrame.Layout.Layout layout)
{
	[Fact]
	public void StacksGeometricsBasedOnId()
	{
		List<TestGeometric> components = TestGeometric.CreateList(3);
		List<TestGeometric> reversed = [.. components];
		reversed.Reverse();

		foreach(var component in reversed)
		{
			Assert.True(layout.AddChild(component));
			Assert.True(Compare.Same(component.Geometry, layout.Geometry));
		}

		var compEnum = components.GetEnumerator();
		var layEnum = layout.Children.GetEnumerator();
		while(compEnum.MoveNext() && layEnum.MoveNext())
		{
			Assert.Equal(compEnum.Current, layEnum.Current);
		}
		Assert.False(compEnum.MoveNext());
		Assert.False(layEnum.MoveNext());
	}

	[Fact]
	public void StacksGeometricsBasedOnLayer()
	{
		List<TestGeometric> components = TestGeometric.CreateList(3);

		int layer = 3;
		foreach(var component in components)
		{
			component.Layer = layer;
			--layer;
		}

		foreach(var component in components)
		{
			Assert.True(layout.AddChild(component));
			Assert.True(Compare.Same(component.Geometry, layout.Geometry));
		}

		List<TestGeometric> reversed = [.. components];
		reversed.Reverse();

		var compEnum = reversed.GetEnumerator();
		var layEnum = layout.Children.GetEnumerator();
		while(compEnum.MoveNext() && layEnum.MoveNext())
		{
			Assert.Equal(compEnum.Current, layEnum.Current);
		}
		Assert.False(compEnum.MoveNext());
		Assert.False(layEnum.MoveNext());
	}

	[Fact]
	public void StacksGeometricsBasedOnIdAndLayer()
	{
		Random rand = new();

		List<TestGeometric> components = [
				new(0), // id 0, layer 0
				new(0), // id 1, layer 0
				new(1), // id 2, layer 1
				new(1), // id 3, layer 1
				new(1), // id 4, layer 1
				new(2), // id 5, layer 2
				new(2)  // id 6, layer 2
			];
		List<TestGeometric> shuffled = [.. components];
		shuffled.Shuffle(rand);

		foreach(var component in shuffled)
		{
			Assert.True(layout.AddChild(component));
		}

		var compEnum = components.GetEnumerator();
		var layEnum = layout.Children.GetEnumerator();
		while(compEnum.MoveNext() && layEnum.MoveNext())
		{
			Assert.Equal(compEnum.Current, layEnum.Current);
		}
		Assert.False(compEnum.MoveNext());
		Assert.False(layEnum.MoveNext());
	}
}
