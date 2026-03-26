using GameFrame.Layout;
using TestGameFrame.Utils;

namespace TestGameFrame.Layout;
public class CanvasLayoutTests() : LayoutTestBase(new CanvasLayout())
{
	[Fact]
	public void CanvasLayoutMaintainsRelativeGeometry()
	{
		CanvasLayout layout = new()
		{
			X = 2,
			Y = 2,
			Width = 10,
			Height = 10
		};

		TestGeometric component = new(1)
		{
			X = 7,
			Y = 7,
			Width = 2,
			Height = 2
		};

		Assert.True(layout.AddChild(component));

		layout.ArrangeChildren();

		Assert.Equal(7, component.X);
		Assert.Equal(7, component.Y);
		Assert.Equal(2, component.Width);
		Assert.Equal(2, component.Height);

		layout.X = 7;
		layout.Y = 7;
		layout.Width = 20;
		layout.Height = 20;

		layout.ArrangeChildren();

		Assert.Equal(17, component.X);
		Assert.Equal(17, component.Y);
		Assert.Equal(4, component.Width);
		Assert.Equal(4, component.Height);
	}
}
