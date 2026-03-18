using GameFrame.Core;
using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Core.Input;

public partial class Keyboard : IComponent, IUpdate
{
	public IComponent? Parent => _root;

	public ulong Id { get; }

	public int Layer
	{
		get => int.MaxValue;
		set { }
	}
	public bool Enabled { get; set; }

	public bool HasChildren => true;

	public Keyboard(IComponent root)
	{
		_root = root;
		Id = Identity.GenerateId();

		InitializeKeys();
	}

	public bool AddChild(IComponent component) => false;
	public bool RemoveChild(IComponent component) => false;
	public void Invalidate() { }

	public void Update(GameTime time)
	{
		KeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
	}

	public bool IsKeyDown(Keys key) =>
		KeyState.IsKeyDown(key);
	public bool IsKeyUp(Keys key) =>
		KeyState.IsKeyUp(key);

	private KeyboardState KeyState;

	private readonly IComponent _root;
}
