using GameFrame.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GameFrame.Core.Components;
/// <summary>
/// A <see cref="Component"/> with multiple children.
/// </summary>
/// <remarks>
/// <inheritdoc cref="Component" path="/remarks"/>
/// </remarks>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public abstract class Branch(IComponent? parent = null) : Component(parent)
{
	public override IEnumerable<IComponent> Children =>
		_cache ??= [.. _children.OrderBy(c => c.Layer).ThenBy(c => c.Id)];

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

	private IComponent[]? _cache = null;
	private readonly HashSet<IComponent> _children = [];
}
