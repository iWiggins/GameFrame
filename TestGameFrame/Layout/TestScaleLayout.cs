using GameFrame.Layout;
using TestGameFrame.Utils;

namespace TestGameFrame.Layout;
public class TestScaleLayout : TestLayout
{
	public TestScaleLayout() : base(new ScaleLayout())
	{ }

	[Fact]
	public void ScalesPerfectGeometric()
	{
		ScaleLayout layout = new()
		{
			X = 0,
			Y = 0,
			Width = 200,
			Height = 100
		};

		TestGeometric component = new()
		{
			X = 5,
			Y = 10,
			Width = 20,
			Height = 10
		};

		Assert.True(layout.AddChild(component));

		Assert.True(Compare.Same(layout.Geometry, component.Geometry));
	}

	[Fact]
	public void ScalesTallGeometric()
	{
		ScaleLayout layout = new()
		{
			X = 0,
			Y = 0,
			Width = 200,
			Height = 100
		};

		TestGeometric component = new()
		{
			X = 0,
			Y = 0,
			Width = 10,
			Height = 20
		};

		Assert.True(layout.AddChild(component));

		Assert.Equal(75, component.X);
		Assert.Equal(0, component.Y);
		Assert.Equal(50, component.Width);
		Assert.Equal(100, component.Height);
	}

	[Fact]
	public void ScalesWideGeometric()
	{
		ScaleLayout layout = new()
		{
			X = 0,
			Y = 0,
			Width = 200,
			Height = 100
		};

		TestGeometric component = new()
		{
			X = 0,
			Y = 0,
			Width = 40,
			Height = 10
		};

		Assert.True(layout.AddChild(component));

		Assert.Equal(0, component.X);
		Assert.Equal(25, component.Y);
		Assert.Equal(200, component.Width);
		Assert.Equal(50, component.Height);
	}
}
