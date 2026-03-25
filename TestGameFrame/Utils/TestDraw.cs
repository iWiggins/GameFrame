using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace TestGameFrame.Utils;
internal class TestDrawLeaf(ulong id, Action<IComponent> signal) : TestLeaf(id), IDraw
{
	public bool Drawn { get; private set; } = false;
	public void Reset()
	{
		Drawn = false;
	}
	public void Draw(SpriteBatch spriteBatch)
	{
		signal(this);
		Drawn = true;
	}
}

internal class TestDrawBranch(ulong id, Action<IComponent> signal) : TestBranch(id), IDraw
{
	public bool Drawn { get; private set; } = false;
	public void Reset()
	{
		Drawn = false;
	}
	public void Draw(SpriteBatch spriteBatch)
	{
		signal(this);
		Drawn = true;
	}
}