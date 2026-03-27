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
	public int Current => _current;

	public void Advance()
	{
		Child.RemoveChild(_options[_current]);
		_current += 1;
		if(_current >= _options.Length) _current = 0;
		Child.AddChild(_options[_current]);
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

	readonly IComponent[] _options = [.. options];
	int _current = 0;
}
