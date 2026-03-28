namespace GameFrame.Core.Interfaces;
/// <summary>
/// Components which require initialization before the frame runs.
/// </summary>
public interface IInitialize
{
	/// <summary>
	/// Whether this component has been initialized.
	/// </summary>
	public bool Initialized { get; }

	/// <summary>
	/// Initialize the component.
	/// </summary>
	void Initialize();
}
