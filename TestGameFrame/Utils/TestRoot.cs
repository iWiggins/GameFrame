using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace TestGameFrame.Utils;
internal class TestRoot(ulong id) : IRoot
{
	public IComponent? Parent => null;

	public ulong Id => id;

	public int Layer { get; set; } = 0;
	public bool Enabled { get; set; } = true;

	public IEnumerable<IComponent> Children => _children;

	public bool HasChildren => _children.Count > 0;

	public bool AddChild(IComponent component) => _children.Add(component);

	public void AddKeyboard(IComponent keyboard)
	{ }
	public void AddMouse(IComponent mouse)
	{ }

	public void Invalidate()
	{
		foreach(var child in _children) child.Invalidate();
	}
	public bool RemoveChild(IComponent component) => _children.Remove(component);

	public void StartDrawing(SpriteBatch spriteBatch)
	{ }

	public void EndDrawing(SpriteBatch spriteBatch)
	{ }

	private readonly HashSet<IComponent> _children = [];
}
