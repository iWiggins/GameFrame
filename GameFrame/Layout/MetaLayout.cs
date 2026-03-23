using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrame.Core.Interfaces;

namespace GameFrame.Layout;

/// <summary>
/// A base class for layouts carrying metadata about their children.
/// </summary>
public abstract class MetaLayout<T>: Layout
{
	protected void AddEntry(IComponent component, T entry) =>
		_metadata[component] = entry;

	protected T GetEntry(IComponent component) =>
		_metadata[component];

	public override bool RemoveChild(IComponent component) =>
		base.RemoveChild(component) && _metadata.Remove(component);

	protected IEnumerable<T> Entries => _metadata.Values;

	private readonly Dictionary<IComponent, T> _metadata;
}
