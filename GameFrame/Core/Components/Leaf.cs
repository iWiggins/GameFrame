using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Core.Components;
/// <summary>
/// Base class for a <see cref="Component"/> with no children.
/// </summary>
/// <remarks>
/// <inheritdoc cref="Component" path="/remarks"/>
/// </remarks>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class Leaf(IComponent? parent = null) : Component(parent)
{
	public override IEnumerable<IComponent> Children => [];

	public override bool HasChildren => false;

	public override bool AddChild(IComponent component) => false;
	public override bool RemoveChild(IComponent component) => false;
	public override void Invalidate() { }
}
