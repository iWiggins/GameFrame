using GameFrame.Core.Clickables;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Components.Menu;
public class Multiselector(IEnumerable<IComponent> options, IComponent? parent = null, int layer = 0) :
	ClickableTwig<FillLayout>(new(), parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;

	public IComponent CurrentComponent => _options[_current];
	public int Current
	{
		get => _current;
		set
		{
			if(value > 0 && value < _options.Length)
			{
				SwitchChild(_current, value);
				_current = value;
			}
		}
	}

	public void Advance()
	{
		int next = _current + 1;
		if(next >= _options.Length) next = 0;
		SwitchChild(_current, next);
		_current = next;
	}

	public void Initialize()
	{
		Child.AddChild(_options[0]);

		Initialized = true;
	}

	protected override bool OnReleased(Mouse.Buttons button, Point position, double duration)
	{
		Advance();

		return true;
	}

	private void SwitchChild(int oldSelection, int newSelection)
	{
		Child.RemoveChild(_options[oldSelection]);
		Child.AddChild(_options[newSelection]);
	}

	readonly IComponent[] _options = [.. options];
	int _current = 0;
}
