using GameFrame.Components.Images;
using GameFrame.Core.Clickables;
using GameFrame.Core.Components;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameFrame.Components.Menu;
/// <summary>
/// A UI component which changes between a selection of components when it is clicked and exposes events for the changes.
/// </summary>
public class Multiselector: ClickableTwig<FillLayout>, IMarginProvider
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

	public double Margins
	{
		set
		{
			Invalidate();
			_margins.Margins = value;
		}
	}

	public double MarginLeft
	{
		get => Child.MarginLeft;
		set
		{
			Invalidate();
			_margins.MarginLeft = value;
		}
	}

	public double MarginRight
	{
		get => Child.MarginRight;
		set
		{
			Invalidate();
			_margins.MarginRight = value;
		}
	}

	public double MarginTop
	{
		get => Child.MarginTop;
		set
		{
			Invalidate();
			_margins.MarginTop = value;
		}
	}

	public double MarginBottom
	{
		get => _margins.MarginBottom;
		set
		{
			Invalidate();
			_margins.MarginBottom = value;
		}
	}

	/// <param name="options">The components to switch between.</param>
	/// <param name="backgroundImage">An optional background image.</param>
	/// <param name="scale">Whether the background should be scaled instead of stretched.</param>
	/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
	public Multiselector(
		IEnumerable<IComponent> options,
		Texture2D? backgroundImage,
		bool scale = false,
		IComponent? parent = null):
		base(new(), null, parent)
	{
		this._backgroundTexture = backgroundImage;
		this._scale = scale;
		_options = [.. options];
		_current = 0;

		if(backgroundImage is not null)
		{
			if(scale)
			{
				FramedImage background = new(backgroundImage);
				Child.AddChild(background);
			}
			else
			{
				Image background = new(backgroundImage);
				Child.AddChild(background);
			}
		}

		IComponent container;
		if(scale)
		{
			ScaleLayout layout = new();
			container = layout;
			_margins = layout;
		}
		else
		{
			FillLayout layout = new();
			container = layout;
			_margins = layout;
		}

		Child.AddChild(container);
		foreach(var option in _options)
		{
			option.Layer = 2;
			container.AddChild(option);
			option.Enabled = false;
		}
		_options[_current].Enabled = true;

		Initialized = true;
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

	readonly IComponent[] _options;
	private readonly Texture2D? _backgroundTexture;
	private readonly bool _scale;
	private readonly IMarginProvider _margins;
	int _current;
}
