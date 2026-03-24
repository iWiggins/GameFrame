using GameFrame.Layout;
using TestGameFrame.Utils;
using static GameFrame.Layout.FlowLayout;

namespace TestGameFrame.Layout;

public abstract class FlowLayoutTestBase(FlowLayout.Direction direction) : TestLayout(new FlowLayout(direction))
{
	[Theory]
	[InlineData(1, 1, 1)]
	[InlineData(1)]
	[InlineData(1, 2, 1)]
	public void ComponentsFlow(params int[] proportions)
	{
		int count = proportions.Length;
		int cellSize = 100;
		int width;
		int height;
		switch(direction)
		{
			case FlowLayout.Direction.Right:
			case FlowLayout.Direction.Left:
				width = count * cellSize;
				height = cellSize;
				break;
			case FlowLayout.Direction.Down:
			case FlowLayout.Direction.Up:
				width = cellSize;
				height = count * cellSize;
				break;
			default:
				throw new ArgumentException("Invalid direction passed to test.");
		}
		FlowLayout layout = new(direction)
		{
			X = 0,
			Y = 0,
			Width = width,
			Height = height
		};

		List<TestGeometric> components = TestGeometric.CreateList(count);

		int index = 0;
		foreach(var component in components)
		{
			layout.AddChild(component, proportions[index++]);
		}

		layout.ArrangeChildren();

		int x;
		int y;
		int totalProportions = proportions.Sum();
		int unitSize = direction switch
		{
			Direction.Right => width / totalProportions,
			Direction.Left => width / totalProportions,
			Direction.Down => height / totalProportions,
			Direction.Up => height / totalProportions,
			_ => throw new ArgumentException("Invalid direction passed to test.")
		};
		switch(direction)
		{
			case FlowLayout.Direction.Right:
				x = 0;
				y = 0;
				break;
			case FlowLayout.Direction.Left:
				x = width;
				y = 0;
				break;
			case FlowLayout.Direction.Down:
				x = 0;
				y = 0;
				break;
			case FlowLayout.Direction.Up:
				x = 0;
				y = height;
				break;
			default:
				throw new ArgumentException("Invalid direction passed to test.");
		}
		void Traverse(int proportion)
		{
			switch(direction)
			{
				case FlowLayout.Direction.Right:
					x += unitSize * proportion;
					break;
				case FlowLayout.Direction.Left:
					x -= unitSize * proportion;
					break;
				case FlowLayout.Direction.Down:
					y += unitSize * proportion;
					break;
				case FlowLayout.Direction.Up:
					y -= unitSize * proportion;
					break;
				default:
					throw new ArgumentException("Invalid direction passed to test.");
			}
		}

		index = 0;
		foreach(var component in components)
		{
			int proportion = proportions[index++];
			int xoffset = direction == Direction.Left ? unitSize * proportion : 0;
			int yoffset = direction == Direction.Up ? unitSize * proportion : 0;
			Assert.Equal(x - xoffset, component.X);
			Assert.Equal(y - yoffset, component.Y);

			switch(direction)
			{
				case FlowLayout.Direction.Right:
				case FlowLayout.Direction.Left:
					Assert.Equal(unitSize * proportion, component.Width);
					Assert.Equal(height, component.Height);
					break;
				case FlowLayout.Direction.Down:
				case FlowLayout.Direction.Up:
					Assert.Equal(width, component.Width);
					Assert.Equal(unitSize * proportion, component.Height);
					break;
				default:
					throw new ArgumentException("Invalid direction passed to test.");
			}

			Traverse(proportion);
		}

	}
}

public class RightFlowTest() : FlowLayoutTestBase(FlowLayout.Direction.Right)
{ }

public class LeftFlowTest() : FlowLayoutTestBase(FlowLayout.Direction.Left)
{ }

public class DownFlowTest() : FlowLayoutTestBase(FlowLayout.Direction.Down)
{ }
public class UpFlowTest() : FlowLayoutTestBase(FlowLayout.Direction.Up)
{ }