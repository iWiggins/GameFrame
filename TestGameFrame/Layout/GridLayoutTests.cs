using GameFrame.Layout;
using TestGameFrame.Utils;

namespace TestGameFrame.Layout;
public abstract class GridLayoutTestBase(int columns, int rows): LayoutTestBase(new GridLayout(columns, rows))
{

	protected int Columns => columns;
	protected int Rows => rows;

	protected abstract IEnumerable<GridLayout.Cell> GetCells();

	[Fact]
	public void ComponentsAlign()
	{
		GridLayout layout = new(columns, rows)
		{
			X = 0,
			Y = 0,
			Width = 1200,
			Height = 1200
		};

		ulong id = layout.Id + 1;

		var components =
			(from cell in GetCells()
			 select new { component = new TestGeometric(id++), cell })
			 .ToList();

		foreach(var pair in components)
		{
			Assert.True(layout.AddChild(pair.component, pair.cell.Column, pair.cell.Row, pair.cell.ColumnSpan, pair.cell.RowSpan));
		}

		layout.ArrangeChildren();

		int cellWidth = layout.Width / columns;
		int cellHeight = layout.Height / rows;

		foreach(var pair in components)
		{
			var component = pair.component;
			var cell = pair.cell;

			int xOffset = cell.Column * cellWidth;
			int yOffset = cell.Row * cellHeight;

			int x = layout.X + xOffset;
			int y = layout.Y + yOffset;

			int width = cellWidth * cell.ColumnSpan;
			int height = cellHeight * cell.RowSpan;

			Assert.Equal(x, component.X);
			Assert.Equal(y, component.Y);
			Assert.Equal(width, component.Width);
			Assert.Equal(height, component.Height);
		}
	}
}

public class TestSimpleGridLayout() : GridLayoutTestBase(3, 3)
{
	protected override IEnumerable<GridLayout.Cell> GetCells()
	{
		for(int col = 0; col < Columns; ++col)
		{
			for(int row = 0; row < Rows; ++row)
			{
				yield return new(col, row, 1, 1);
			}
		}
	}
}

public class TestGridLayoutWithSpans() : GridLayoutTestBase(4, 4)
{
	protected override IEnumerable<GridLayout.Cell> GetCells()
	{
		return [
			new(1,0, 2, 1),
			new(0, 1, 1, 2)
			];
	}
}