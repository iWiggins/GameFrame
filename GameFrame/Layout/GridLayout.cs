using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Layout;

/// <summary>
/// Spatially arranges components in a grid with specified rows and columns.
/// </summary>
/// <param name="columns"><inheritdoc cref="GridLayout.Columns" path="/summary"/></param>
/// <param name="rows"><inheritdoc cref="GridLayout.Rows" path="/summary"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class GridLayout(int columns, int rows, IComponent? parent = null) : MetaLayout<GridLayout.Cell>(parent)
{
	/// <summary>
	/// Cell metadata about a child component of the grid.
	/// </summary>
	/// <param name="column"><inheritdoc cref="Cell.Column" path="/summary"/></param>
	/// <param name="row"><inheritdoc cref="Cell.Row" path="/summary"/></param>
	/// <param name="columnSpan"><inheritdoc cref="Cell.ColumnSpan" path="/summary"/></param>
	/// <param name="rowSpan"><inheritdoc cref="Cell.RowSpan" path="/summary"/></param>
	public class Cell(int column, int row, int columnSpan, int rowSpan)
	{
		/// <summary>
		/// The component's column.
		/// </summary>
		public int Column { get; set; } = column;
		/// <summary>
		/// The component's row.
		/// </summary>
		public int Row { get; set; } = row;
		/// <summary>
		/// How many columns the component takes up.
		/// </summary>
		public int ColumnSpan { get; set; } = columnSpan;
		/// <summary>
		/// How many rows the component takes up.
		/// </summary>
		public int RowSpan { get; set; } = rowSpan;
	}

	/// <summary>
	/// The number of columns in the grid.
	/// </summary>
	public int Columns => columns;
	/// <summary>
	/// The number of rows in the grid.
	/// </summary>
	public int Rows => rows;

	/// <summary>
	/// Adds a child component at cell (0,0).
	/// </summary>
	/// <param name="component"><inheritdoc cref="Layout.AddChild" path="/param[@name='component']"/></param>
	/// <returns><inheritdoc cref="Layout.AddChild" path="/returns"/></returns>
	public override bool AddChild(IComponent component)
	{
		Invalidate();
		if(component is IGeometric)
		{
			return AddChild(component, 0, 0);
		}
		else
		{
			// don't bother adding metadata if not geometric.
			return base.AddChild(component);
		}
	}

	/// <summary>
	/// Adds a child component with a specific row and column.
	/// </summary>
	/// <param name="component"><inheritdoc cref="Layout.AddChild" path="/param[@name='component']"/></param>
	/// <param name="column">The column within the grid.</param>
	/// <param name="row">The row within the grid.</param>
	/// <param name="columnSpan">The number of columns the component should take up (default 1)</param>
	/// <param name="rowSpan">The number of rows the component should take up (default 1)</param>
	/// <returns><inheritdoc cref="Layout.AddChild" path="/returns"/></returns>
	/// <exception cref="ArgumentException">Throws if the component has no geometry.</exception>
	public bool AddChild(IComponent component, int column, int row, int columnSpan = 1, int rowSpan = 1)
	{
		if(component is not IGeometric)
		{
			throw new ArgumentException("Component is not geometric. Use AddChild with no metadata to add it as a child of grid layout.");
		}

		Invalidate();

		if(base.AddChild(component))
		{
			AddEntry(component, new(column, row, columnSpan, rowSpan));
			return true;
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Moves a child component within the grid.
	/// </summary>
	/// <param name="component">The component to move.</param>
	/// <param name="column">The column to move the component to.</param>
	/// <param name="row">The row to move the component to.</param>
	/// <param name="columnSpan">The number of columns the component should take up.</param>
	/// <param name="rowSpan">The number of rows the component should take up.</param>
	/// <returns>Whether or not the child is present in the layout.</returns>
	public bool MoveChild(IComponent component, int column, int row, int columnSpan = 1, int rowSpan = 1)
	{
		if(Contains(component))
		{
			var cell = GetEntry(component);
			cell.Column = column;
			cell.Row = row;
			cell.ColumnSpan = columnSpan;
			cell.RowSpan = rowSpan;
			return true;
		}
		else
		{
			return false;
		}
	}

	protected override IEnumerable<IComponent> Arrange()
	{
		int cellWidth = Width / columns;
		int cellHeight = Height / rows;

		foreach(var component in _children)
		{
			if(component is IGeometric geometric)
			{
				var cell = GetEntry(component);
				int xOffset = cellWidth * cell.Column;
				int yOffset = cellHeight * cell.Row;
				int width = cellWidth * cell.ColumnSpan;
				int height = cellHeight * cell.RowSpan;
				int x = X + xOffset;
				int y = Y + yOffset;

				geometric.Geometry = new(x, y, width, height);
			}
		}

		return Order();
	}

}
