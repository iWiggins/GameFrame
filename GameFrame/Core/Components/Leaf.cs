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
/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
public abstract class Leaf(IComponent? parent = null, int layer = 0) : Component(parent, layer)
{
	public override IEnumerable<IComponent> Children => [];

	public override bool HasChildren => false;

	public override bool AddChild(IComponent component) => false;
	public override bool RemoveChild(IComponent component) => false;
	public override void Invalidate() { }
}
