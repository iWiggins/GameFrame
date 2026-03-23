using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Layout;

/// <summary>
/// A Layout which only orders components by layer and creation order.
/// </summary>
public class StackLayout : Layout
{
	protected override IEnumerable<IComponent> Arrange() =>
		Order();
}
