using GameFrame.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Core.Components;
/// <summary>
/// A base class for a <see cref="Component"/> with multiple children.
/// </summary>
public class Branch : Component
{
	protected Branch(IComponent? parent = null, int layer = 0):
		base(parent, layer)
	{
		_children = [];
		_cache = null;
	}
	public override IEnumerable<IComponent> Children =>
		_cache ??= [.. _children.OrderByDescending(c => c.Layer).ThenBy(c => c.Id)];

	public override bool HasChildren => _children.Count > 0;

	public override bool AddChild(IComponent component)
	{
		if(_children.Add(component))
		{
			_cache = null;
			return true;
		}
		else
		{
			return false;
		}
	}
	public override bool RemoveChild(IComponent component)
	{
		if(_children.Remove(component))
		{
			_cache = null;
			return true;
		}
		else
		{
			return false;
		}
	}
	public override void Invalidate() => _cache = null;

	private List<IComponent>? _cache;
	private readonly HashSet<IComponent> _children;
}
