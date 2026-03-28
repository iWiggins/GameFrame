using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace TestGameFrame.Utils;
internal class TestUpdateLeaf(ulong id, Action<IComponent> signal) : TestLeaf(id), IUpdate
{
	public bool Updated { get; private set; } = false;
	public void Reset()
	{
		Updated = false;
	}

	public void Update(GameTime time)
	{
		Updated = true;
		signal(this);
	}
}

internal class TestUpdateBranch(ulong id, Action<IComponent> signal) : TestBranch(id), IUpdate
{
	public bool Updated { get; private set; } = false;
	public void Reset()
	{
		Updated = false;
	}

	public void Update(GameTime time)
	{
		Updated = true;
		signal(this);
	}
}