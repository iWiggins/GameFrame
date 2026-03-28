using GameFrame.Core.Clickables;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Components.Menu;
public class Multiselector(IEnumerable<IComponent> options, IComponent? parent = null, int layer = 0) :
	ClickableTwig<FillLayout>(new(), parent, layer), IInitialize, IDraw
{
	public delegate void OnCurrentChanged(int oldValue, int newValue);
	public OnCurrentChanged? Changed;

	public delegate void OnComponentChanged(IComponent oldComponent, IComponent newComponent);
	public OnComponentChanged? ComponentChanged;

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
		if(Changed is not null) Changed(oldSelection, newSelection);
		if(ComponentChanged is not null) ComponentChanged(_options[oldSelection], _options[newSelection]);
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		// for debugging
		// Source - https://stackoverflow.com/a/31316757
		// Posted by Zillo, modified by community. See post 'Timeline' for change history
		// Retrieved 2026-03-27, License - CC BY-SA 4.0
		{
			Color[] data = new Color[Width * Height];
			Texture2D rectTexture = new Texture2D(spriteBatch.GraphicsDevice, Width, Height);

			for(int i = 0; i < data.Length; ++i)
				data[i] = Color.White;

			rectTexture.SetData(data);
			var position = new Vector2(Left, Top);

			spriteBatch.Draw(rectTexture, position, Color.Red);
		}
		{
			Color[] data = new Color[Child.Width * Child.Height];
			Texture2D rectTexture = new Texture2D(spriteBatch.GraphicsDevice, Child.Width, Child.Height);

			for(int i = 0; i < data.Length; ++i)
				data[i] = Color.White;

			rectTexture.SetData(data);
			var position = new Vector2(Child.Left, Child.Top);

			spriteBatch.Draw(rectTexture, position, Color.Blue);
		}
	}

	readonly IComponent[] _options = [.. options];
	int _current = 0;
}
