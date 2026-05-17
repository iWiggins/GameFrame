namespace GameFrame.Core.Interfaces;
public interface IMarginProvider
{
	/// <summary>
	/// The proportional space between the sides of the component and the children.
	/// </summary>
	public double Margins { set; }
	/// <summary>
	/// The proportional space between the left of the component and the children.
	/// </summary>
	public double MarginLeft { get; set; }
	/// <summary>
	/// The proportional space between the right of the component and the children.
	/// </summary>
	public double MarginRight { get; set; }
	/// <summary>
	/// The proportional space between the top of the component and the children.
	/// </summary>
	public double MarginTop { get; set; }
	/// <summary>
	/// The proportional space between the bottom of the component and the children.
	/// </summary>
	public double MarginBottom { get; set; }
}
