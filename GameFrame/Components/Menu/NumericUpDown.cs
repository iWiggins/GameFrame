using GameFrame.Components.Buttons;
using GameFrame.Components.Text;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Menu;
public class NumericUpDown(Texture2D up, Texture2D down, SpriteFont number, int min, int max, int start, IComponent? parent = null, int layer = 0) :
	GeometricTwig<FlowLayout>(new(FlowLayout.Direction.Down), parent, layer), IInitialize, IReset
{
	public delegate void OnValueChanged(int oldValue, int newValue);

	public event OnValueChanged? ValueChanged;

	public bool Initialized { get; private set; } = false;

	public Color TextColor
	{
		get => _text.Color;
		set => _text.Color = value;
	}

	public Color ButtonColor
	{
		get => _up.Color;
		set
		{
			_up.Color = value;
			_down.Color = value;
		}
	}

	public int Value
	{
		get => _value;
		set
		{
			if(value >= min && value <= max)
			{
				int old = _value;
				_value = value;
				SetText();
				if(ValueChanged is not null) ValueChanged(old, _value);
			}
		}
	}

	public void Increment()
	{
		if(_value < max)
		{
			_value += 1;
			SetText();
			if(ValueChanged is not null) ValueChanged(_value - 1, Value);
		}
	}

	public void Decrement()
	{
		if(_value > min)
		{
			_value -= 1;
			SetText();
			if(ValueChanged is not null) ValueChanged(_value + 1, Value);
		}
	}

	public void Initialize()
	{
		Child.AddChild(_up);
		_up.Pressed += (b,p) => Increment();

		Child.AddChild(_text);

		Child.AddChild(_down);
		_down.Pressed += (b, p) => Decrement();

		SetText();

		Initialized = true;
	}
	public void Reset()
	{
		_value = start;
		SetText();
	}

	private void SetText() => _text.Contents = _value.ToString();

#pragma warning disable CS9124
	// start needs to be captured for the Reset functionality, but _value is mutated.
	// warning is a false positive.
	private int _value = start;
#pragma warning restore CS9124
	private readonly BoundText _text = new(number);
	private readonly ImageButton _up = new(up);
	private readonly ImageButton _down = new(down);
}
