using GameFrame.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Components;
/// <summary>
/// Base class for a <see cref="Component"/> with exactly one child, set on creation and immutable.
/// </summary>
/// <typeparam name="TChild">The child component's type.</typeparam>
public abstract class Twig<TChild> : Component where TChild : IComponent
{
	/// <summary>
	/// 
	/// </summary>
	/// <param name="child">This <see cref="Twig{T}"/>'s child.</param>
	/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
	/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
	protected Twig(TChild child, IComponent? parent = null, int layer = 0):
		base(parent, layer)
	{
		_child = child;
	}
	public override IEnumerable<IComponent> Children
	{
		get
		{
			yield return _child;
		}
	}

	public override bool HasChildren => true;

	/// <summary>
	/// A non-operation on a <see cref="Twig{T}"/>.
	/// </summary>
	/// <param name="component">Ignored.</param>
	/// <returns>false</returns>
	public override bool AddChild(IComponent component) => false;

	/// <summary>
	/// A non-operation on a <see cref="Twig{T}"/>.
	/// </summary>
	/// <param name="component">Ignored.</param>
	/// <returns>false</returns>
	public override bool RemoveChild(IComponent component) => false;

	public override void Invalidate() => _child.Invalidate();

	/// <summary>
	/// This <see cref="Twig{T}"/>'s child.
	/// </summary>
	public TChild Child => _child;

	private readonly TChild _child;
}
