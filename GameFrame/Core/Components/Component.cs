using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Core.Components;
/// <summary>
/// A minimal component base class.
/// </summary>
/// <remarks>
/// ABCs in the <see cref="GameFrame.Core.Components"/> namespace ease development
/// of custom components by implementing the minimal boilerplate needed for components.
/// </remarks>
/// <param name="parent">This component's parent (default null).</param>
/// <param name="layer">This component's layer (default 0).</param>
public abstract class Component(IComponent? parent = null, int layer = 0) : IComponent
{
	public IComponent? Parent { get; } = parent;
	public ulong Id { get; } = Identity.GenerateId();
	public int Layer
	{
		get => layer;
		set
		{
			layer = value;
			Parent?.Invalidate();
		}
	}
	public bool Enabled { get; set; } = true;

	public abstract IEnumerable<IComponent> Children { get; }

	public abstract bool HasChildren { get; }

	public abstract bool AddChild(IComponent component);
	public abstract bool RemoveChild(IComponent component);

	public abstract void Invalidate();
}
