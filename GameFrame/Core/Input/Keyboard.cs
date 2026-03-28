using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameFrame.Core.Input;

/// <summary>
/// A component that manages the keyboard.
/// Contains public <see cref="Key"/> properties for every keyboard key.
/// </summary>
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

	/// <param name="root">The root component of the frame this keyboard is in.</param>
	public Keyboard(IRoot root)
	{
		_root = root;
		Id = Identity.GenerateId();
		Enabled = true;
		InitializeKeys();
	}

	public bool AddChild(IComponent component) => false;
	public bool RemoveChild(IComponent component) => false;
	public void Invalidate() { }

	public void Update(GameTime time)
	{
		KeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
	}

	/// <summary>
	/// Checks if the specified key is down.
	/// </summary>
	/// <param name="key">The key to query.</param>
	/// <returns>Whether the key is down.</returns>
	public bool IsKeyDown(Keys key) =>
		KeyState.IsKeyDown(key);

	/// <summary>
	/// Checks whether the specified key is up.
	/// </summary>
	/// <param name="key">The key to query.</param>
	/// <returns>Whether the key is up.</returns>
	public bool IsKeyUp(Keys key) =>
		KeyState.IsKeyUp(key);

	private KeyboardState KeyState;

	private readonly IRoot _root;
}
