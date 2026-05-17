using GameFrame.Core.Components;
using GameFrame.Components.Buttons;
using GameFrame.Components.Text;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameFrame.Components.Images;

namespace GameFrame.Components.Menu;
/// <summary>
/// A ui component representing a number with arrows to tick it up and down.
/// </summary>
public class NumericUpDown: GeometricTwig<FlowLayout>, IReset
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
	/// The color of the background.
	/// </summary>
	/// <remarks>
	/// This may not exist if the background does not exist.
	/// </remarks>
	public Color BackgroundColor
	{
		get
		{
			if(_bgImage is not null)
			{
				return _bgImage.Color;
			}
			else if(_fbgImage is not null)
			{
				return _fbgImage.Color;
			}
			else
			{
				return Color.White;
			}
		}
		set
		{
			if(_bgImage is not null)
			{
				_bgImage.Color = value;
			}
			else if(_fbgImage is not null)
			{
				_fbgImage.Color = value;
			}
		}
	}

	/// <summary>
	/// The maximum number.
	/// </summary>
	public int Min => _min;
	/// <summary>
	/// The minimum number.
	/// </summary>
	public int Max => _max;

	/// <summary>
	/// The current value.
	/// </summary>
	public int Value
	{
		get => _value;
		set
		{
			if(value >= _min && value <= _max)
			{
				int old = _value;
				_value = value;
				SetText();
				if(ValueChanged is not null) ValueChanged(old, _value);
			}
		}
	}


	/// <summary>
	/// <inheritdoc cref="NumericUpDown" path="/summary"/>
	/// </summary>
	/// <param name="up">Texture for the up arrow.</param>
	/// <param name="down">Texture for the down arrow.</param>
	/// <param name="number">Font for the number.</param>
	/// <param name="min"><inheritdoc cref="NumericUpDown.Min" path="/summary"/></param>
	/// <param name="max"><inheritdoc cref="NumericUpDown.Max" path="/summary"/></param>
	/// <param name="start">The starting number.</param>
	/// <param name="backgroundImage">An optional background image.</param>
	/// <param name="scale">Whether the background should be scaled instead of stretched.</param>
	/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
	public NumericUpDown(
		Texture2D up,
		Texture2D down,
		SpriteFont number,
		int min,
		int max,
		int start,
		Texture2D? background = null,
		bool scale = false,
		IComponent? parent = null):
		base(new(FlowLayout.Direction.Down), parent)
	{
		_min = min;
		_max = max;
		_start = start;
		_value = start;

		_up = new(up);
		Child.AddChild(_up);
		_up.Pressed += (b, p) => Increment();

		FillLayout stack = new();
		Child.AddChild(stack);

		if(background is not null)
		{
			if(scale)
			{
				_fbgImage = new(background)
				{
					Layer = -1
				};
				stack.AddChild(_fbgImage);
			}
			else
			{
				_bgImage = new(background)
				{
					Layer = -1
				};
				stack.AddChild(_bgImage);
			}
		}

		_text = new(number);

		stack.AddChild(_text);

		_down = new(down);

		Child.AddChild(_down);
		_down.Pressed += (b, p) => Decrement();

		SetText();

		Initialized = true;
	}

	/// <summary>
	/// Increments the current value by 1 if it is below max.
	/// </summary>
	public void Increment()
	{
		if(_value < _max)
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
		if(_value > _min)
		{
			_value -= 1;
			SetText();
			if(ValueChanged is not null) ValueChanged(_value + 1, Value);
		}
	}

	public void Reset()
	{
		_value = _start;
		SetText();
	}

	private void SetText() => _text.Contents = _value.ToString();

	private int _start;
	private int _min;
	private int _max;
	private int _value;

	private readonly BoundText _text;
	private readonly ImageButton _up;
	private readonly ImageButton _down;
	private readonly Image? _bgImage;
	private readonly FramedImage? _fbgImage;
}
