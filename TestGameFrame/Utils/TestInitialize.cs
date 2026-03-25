using GameFrame.Core.Interfaces;

namespace TestGameFrame.Utils;

internal class TestInitializeLeaf(ulong id, Action<IComponent> signal) : TestLeaf(id), IInitialize
{
	public bool Initialized { get; private set; } = false;
	public void Initialize()
	{
		signal(this);
		Initialized = true;
	}

	public void Reset()
	{
		Initialized = false;
	}
}

internal class TestInitializeBranch(ulong id, Action<IComponent> signal) : TestBranch(id), IInitialize
{
	public bool Initialized { get; private set; } = false;
	public void Initialize()
	{
		signal(this);
		Initialized = true;
	}

	public void Reset()
	{
		Initialized = false;
	}
}