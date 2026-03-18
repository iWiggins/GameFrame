using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Core.Components;
public abstract class Component : IComponent
{
	/// <summary>
	/// Constructs a component with an optional parent and layer.
	/// </summary>
	/// <param name="parent">This component's parent (default null).</param>
	/// <param name="layer">This component's layer (default 0).</param>
	protected Component(IComponent? parent = null, int layer = 0)
	{
		Id = Identity.GenerateId();
		Enabled = true;
		Parent = parent;
		_layer = layer;
	}
	public IComponent? Parent { get; }
	public ulong Id { get; }
	public int Layer
	{
		get => _layer;
		set
		{
			_layer = value;
			Parent?.Invalidate();
		}
	}
	public bool Enabled { get; set; }

	public abstract IEnumerable<IComponent> Children { get; }

	public abstract bool HasChildren { get; }

	public abstract bool AddChild(IComponent component);
	public abstract bool RemoveChild(IComponent component);

	public abstract void Invalidate();

	private int _layer;
}
