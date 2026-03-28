namespace GameFrame.Core.Interfaces;
/// <summary>
/// Components that need reset state if a frame is navigated back to.
/// </summary>
public interface IReset
{
	/// <summary>
	/// Reset the state of the component.
	/// </summary>
	void Reset();
}
