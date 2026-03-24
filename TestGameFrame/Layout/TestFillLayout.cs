using GameFrame.Layout;
using TestGameFrame.Utils;

namespace TestGameFrame.Layout;

public class TestFillLayout : TestLayout
{
	public TestFillLayout() : base(new FillLayout())
	{ }

	[Fact]
	public void StretchesGeometrics()
	{
		FillLayout layout = new()
		{
			X = 0,
			Y = 0,
			Width = 200,
			Height = 100
		};

		List<TestGeometric> components = TestGeometric.CreateList(3);

		foreach(var component in components)
		{
			Assert.True(layout.AddChild(component));
		}

		layout.ArrangeChildren();

		foreach(var component in components)
		{
			Assert.True(Compare.Same(component.Geometry, layout.Geometry));
		}
	}
}
