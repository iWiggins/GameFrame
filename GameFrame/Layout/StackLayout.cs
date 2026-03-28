using GameFrame.Core.Components;
using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Layout;

/// <summary>
/// A Layout which only orders components by layer and creation order.
/// </summary>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class StackLayout(IComponent? parent = null) : Layout(parent)
{
	protected override IEnumerable<IComponent> Arrange() =>
		Order();
}
