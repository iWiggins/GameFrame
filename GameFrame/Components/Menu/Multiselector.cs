using GameFrame.Core.Clickables;
using GameFrame.Core.Components;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace GameFrame.Components.Menu;
/// <summary>
/// A UI component which changes between a selection of components when it is clicked and exposes events for the changes.
/// </summary>
/// <param name="options">The components to switch between.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class Multiselector(IEnumerable<IComponent> options, IComponent? parent = null) :
	ClickableTwig<FillLayout>([Mouse.Buttons.Left], new(), parent), IInitialize
{
	/// <summary>
	/// Handler for when the current selection has been changed.
	/// </summary>
	/// <param name="oldValue">The previous selected value.</param>
	/// <param name="newValue">The new selected value.</param>
	public delegate void OnCurrentChanged(int oldValue, int newValue);
	/// <summary>
	/// An event raised when the current selection has been changed.
	/// </summary>
	public OnCurrentChanged? Changed;

	/// <summary>
	/// Handler for when the current selection has been changed.
	/// </summary>
	/// <param name="oldComponent">The previous selected component.</param>
	/// <param name="newComponent">The new selected component.</param>

	public delegate void OnComponentChanged(IComponent oldComponent, IComponent newComponent);
	/// <summary>
	/// An event raised when the current selection has been changed.
	/// </summary>
	public OnComponentChanged? ComponentChanged;

	public bool Initialized { get; private set; } = false;

	/// <summary>
	/// The currently selected component.
	/// </summary>
	public IComponent CurrentComponent => _options[_current];
	/// <summary>
	/// The current selection.
	/// </summary>
	public int Current
	{
		get => _current;
		set
		{
			if(value != _current && value > 0 && value < _options.Length)
			{
				SwitchChild(_current, value);
				_current = value;
			}
		}
	}

	/// <summary>
	/// Advance forward to the next selection.
	/// </summary>
	public void Advance()
	{
		int next = _current + 1;
		if(next >= _options.Length) next = 0;
		SwitchChild(_current, next);
		_current = next;
	}

	public void Initialize()
	{
		foreach(var option in _options)
		{
			Child.AddChild(option);
			option.Enabled = false;
		}
		_options[_current].Enabled = true;

		Initialized = true;
	}

	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)
	{
		Advance();

		return true;
	}

	private void SwitchChild(int oldSelection, int newSelection)
	{
		_options[oldSelection].Enabled = false;
		_options[newSelection].Enabled = true;
		if(Changed is not null) Changed(oldSelection, newSelection);
		if(ComponentChanged is not null) ComponentChanged(_options[oldSelection], _options[newSelection]);
	}

	readonly IComponent[] _options = [.. options];
	int _current = 0;
}
