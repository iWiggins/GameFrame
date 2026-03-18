using System.Collections.Generic;

namespace GameFrame.Core.Interfaces;
public interface IComponent
{
	/// <summary>
	/// The component's parent component. Used when needed to signal changes in the child.
	/// </summary>
	IComponent? Parent { get; }
	/// <summary>
	/// The component's ID, set on its creation. This is used to determine the order in which components are kept.
	/// </summary>
	ulong Id { get; }

	/// <summary>
	/// The component's layer. This overrides the ordering of components based on ID.
	/// Modifying the layer should invalidate the parent, so the parent can recalculate
	/// necessary layering logic for its children.
	/// </summary>
	int Layer { get; set; }

	/// <summary>
	/// Whether the component is enabled. If a component is enabled, it will be initialized, updated, or drawn. If it is not, it will be skipped in those processes.
	/// </summary>
	bool Enabled { get; set; }

	/// <summary>
	/// The children of this component.
	/// </summary>
	IEnumerable<IComponent> Children { get; }

	/// <summary>
	/// Returns if this component has children.
	/// </summary>
	bool HasChildren { get; }

	/// <summary>
	/// Add a child to this component.
	/// </summary>
	/// <param name="component">The <see cref="IComponent"/> to add.</param>
	/// <returns>Whether the addition was successful.</returns>
	/// <remarks>
	/// The most common reason for an add to fail is if <paramref name="component"/> is already a child.
	/// </remarks>
	bool AddChild(IComponent component);

	/// <summary>
	/// Removoe a child from this component.
	/// </summary>
	/// <param name="component">The <see cref="IComponent"/> to add.</param>
	/// <returns>Whether the removal was successful.</returns>
	/// <remarks>
	/// The most common reason for a removal to fail is if <paramref name="component"/> is not a child.
	/// </remarks>
	bool RemoveChild(IComponent component);

	/// <summary>
	/// The component has changed state and cached information should be recalculated.
	/// </summary>
	void Invalidate();
}
