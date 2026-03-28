using System.Collections.Generic;

namespace GameFrame.Core.Interfaces;
/// <summary>
/// A component. The base interface of all game objects.
/// </summary>
/// <remarks>
/// Any object which implements this interface and is correctly set as
/// a child of a component in a Frame will automatically execute logic bound
/// to other interfaces, such as <see cref="IUpdate"/> and <see cref="IDraw"/>.
/// </remarks>
public interface IComponent
{
	/// <summary>
	/// The component's parent component.
	/// </summary>
	/// <remarks>
	/// This is rarely used. Most logic flows from parent to child, not the other way around.
	/// The current use is to signal to layout classes that a component's layer has changed,
	/// and the component should be reordered in the draw order.
	/// </remarks>
	IComponent? Parent { get; }

	/// <summary>
	/// The component's unique ID.
	/// </summary>
	ulong Id { get; }

	/// <summary>
	/// The component's layer. This specifies draw order.
	/// Modifying the layer should invalidate the parent,
	/// so the parent can recalculate necessary layering logic for its children.
	/// </summary>
	int Layer { get; set; }

	/// <summary>
	/// Whether the component is enabled.
	/// If a component is enabled, the frame will interact with it via interfaces like <see cref="IUpdate"/>.
	/// If it is not, it and all of its children will be skipped in those processes.
	/// </summary>
	/// <remarks>
	/// Initialization via <see cref="IInitialize"/> does not respect this setting.
	/// </remarks>
	bool Enabled { get; set; }

	/// <summary>
	/// The children of this component.
	/// </summary>
	IEnumerable<IComponent> Children { get; }

	/// <summary>
	/// If this component has children.
	/// </summary>
	bool HasChildren { get; }

	/// <summary>
	/// Add a child to this component.
	/// </summary>
	/// <param name="component">The component to add.</param>
	/// <returns>Whether the addition was successful.</returns>
	/// <remarks>
	/// The most common reason for an add to fail is if <paramref name="component"/> is already a child.
	/// </remarks>
	bool AddChild(IComponent component);

	/// <summary>
	/// Remove a child from this component.
	/// </summary>
	/// <param name="component">The component to add.</param>
	/// <returns>Whether the removal was successful.</returns>
	/// <remarks>
	/// The most common reason for a removal to fail is if <paramref name="component"/> is not a child.
	/// </remarks>
	bool RemoveChild(IComponent component);

	/// <summary>
	/// The component has changed state and cached information should be recalculated.
	/// This should also invalidate this component's children.
	/// </summary>
	void Invalidate();
}
