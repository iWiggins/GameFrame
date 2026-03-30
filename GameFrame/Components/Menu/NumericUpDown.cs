using GameFrame.Core.Components;
using GameFrame.Components.Buttons;
using GameFrame.Components.Text;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components.Menu;
/// <summary>
/// A ui component representing a number with arrows to tick it up and down.
/// </summary>
/// <param name="up">Texture for the up arrow.</param>
/// <param name="down">Texture for the down arrow.</param>
/// <param name="number">Font for the number.</param>
/// <param name="min"><inheritdoc cref="NumericUpDown.Min" path="/summary"/></param>
/// <param name="max"><inheritdoc cref="NumericUpDown.Max" path="/summary"/></param>
/// <param name="start">The starting number.</param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
public class NumericUpDown(Texture2D up, Texture2D down, SpriteFont number, int min, int max, int start, IComponent? parent = null) :
	GeometricTwig<FlowLayout>(new(FlowLayout.Direction.Down), parent), IInitialize, IReset
{
	/// <summary>
	/// A handler for when the value of the component is changed.
	/// </summary>
	/// <param name="oldValue">The previous value before the change.</param>
	/// <param name="newValue">The new value.</param>
	public delegate void OnValueChanged(int oldValue, int newValue);
	/// <summary>
	/// Raised when the value of the component has been changed.
	/// </summary>
	public event OnValueChanged? ValueChanged;

	public bool Initialized { get; private set; } = false;

	/// <summary>
	/// The color of the text for the number.
	/// </summary>
	public Color TextColor
	{
		get => _text.Color;
		set => _text.Color = value;
	}
	/// <summary>
	/// The color of the buttons.
	/// </summary>
	public Color ButtonColor
	{
		get => _up.Color;
		set
		{
			_up.Color = value;
			_down.Color = value;
		}
	}
	/// <summary>
	/// The maximum number.
	/// </summary>
	public int Min => min;
	/// <summary>
	/// The minimum number.
	/// </summary>
	public int Max => max;

	/// <summary>
	/// The current value.
	/// </summary>
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


	/// <summary>
	/// Increments the current value by 1 if it is below max.
	/// </summary>
	public void Increment()
	{
		if(_value < max)
		{
			_value += 1;
			SetText();
			if(ValueChanged is not null) ValueChanged(_value - 1, Value);
		}
	}
	/// <summary>
	/// Decrements the current value by 1 if it is above min.
	/// </summary>
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
