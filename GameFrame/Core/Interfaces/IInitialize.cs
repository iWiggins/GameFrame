namespace GameFrame.Core.Interfaces;
public interface IInitialize
{
	public bool Initialized { get; }
	void Initialize();
}
