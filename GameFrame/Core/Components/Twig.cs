using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Core.Components;
/// <summary>
/// Base class for a <see cref="Component"/> with exactly one child, set on creation and immutable.
/// </summary>
///<remarks>
/// <inheritdoc cref="Component" path="/remarks"/>
/// </remarks>
/// <typeparam name="TChild">The child component's type.</typeparam>
/// <param name="child">This Twig's child.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class Twig<TChild>(TChild child, IComponent? parent = null):
	Component(parent) where TChild : IComponent
{
	public override IEnumerable<IComponent> Children => [child];

	public override bool HasChildren => true;

	/// <summary>
	/// A non-operation on a twig.
	/// </summary>
	/// <param name="component">Ignored.</param>
	/// <returns>false</returns>
	public override bool AddChild(IComponent component) => false;

	/// <summary>
	/// A non-operation on a twig.
	/// </summary>
	/// <param name="component">Ignored.</param>
	/// <returns>false</returns>
	public override bool RemoveChild(IComponent component) => false;

	public override void Invalidate() => child.Invalidate();

	/// <summary>
	/// This twig's child.
	/// </summary>
	public TChild Child => child;
}
