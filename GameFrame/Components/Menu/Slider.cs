using GameFrame.Core.Clickables;
using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Components.Menu;
public class Slider(Texture2D texture, Texture2D sliderTexture, Slider.Direction direction, int positions):
	GeometricComponent, IInitialize, IUpdate, IClick, IDraw
{
	public enum Direction
	{
		Right,
		Left,
		Down,
		Up
	}
	/// <summary>
	/// Arguments for a slider moved event.
	/// </summary>
	/// <param name="oldPosition"><inheritdoc cref="OldPosition" path="/summary"/></param>
	/// <param name="newPosition"><inheritdoc cref="NewPosition" path="/summary"/></param>
	/// <param name="positions"><inheritdoc cref="Positions" path="/summary"/></param>
	public class SliderMovedEventArgs(int oldPosition, int newPosition, int positions): EventArgs
	{
		/// <summary>
		/// The previous position of the slider.
		/// </summary>
		public int OldPosition { get; } = oldPosition;
		/// <summary>
		/// The current position of the slider.
		/// </summary>
		public int NewPosition { get; } = newPosition;
		/// <summary>
		/// The number of possible slider positions.
		/// </summary>
		public int Positions { get; } = positions;
	}
	/// <summary>
	/// Handler for when the slider moves.
	/// </summary>
	public delegate void SliderMovedHandler(IComponent sender, SliderMovedEventArgs args);
	/// <summary>
	/// Raised when the slider moves.
	/// </summary>
	public event SliderMovedHandler? SliderMoved;
	public event ClickablePressedHandler? Pressed;
	public event ClickableReleasedHandler? Released;
	public event ClickableHoveredHandler? Hovered;
	public event ClickableUnhoveredHandler? Unhovered;

	/// <summary>
	/// The color of the slider.
	/// </summary>
	public Color Color { get; set; } = Color.White;
	/// <summary>
	/// The color of the slider cursor.
	/// </summary>
	public Color CursorColor
	{
		get => Cursor.Color;
		set => Cursor.Color = value;
	}
	/// <summary>
	/// The texture drawn for the slider.
	/// </summary>
	public Texture2D Texture { get; set; } = texture;
	/// <summary>
	/// The texture drawn for the slider cursor.
	/// </summary>
	public Texture2D CursorTexture
	{
		get => Cursor.Texture;
		set => Cursor.Texture = value;
	}
	/// <summary>
	/// A reference to the slider's cursor.
	/// </summary>
	public SliderCursor Cursor { get; } = new(sliderTexture);
	public bool Initialized { get; private set; }

	public bool Down => DownButton is not null;
	public Mouse.Buttons? DownButton { get; private set; } = null;
	public bool Hovering { get; private set; } = false;

	public void Initialize()
	{
		MovePosition(0, false);
		Initialized = true;
	}

	public void Update(GameTime time)
	{
		if(Down)
		{
			MoveCursor(true);
		}
	}

	public void Click(GameTime time, Mouse mouse)
	{
		if(Overlaps(mouse.Position))
		{
			_hoverPosition = mouse.Position;
			if(!Hovering)
			{
				Hovering = true;
				hoverTime = time;
				if(Hovered is not null) Hovered(this, new(mouse.Position));
			}
			if(DownButton is null)
			{
				foreach(var button in buttons)
				{
					if(mouse.IsDown(button))
					{
						DownButton = button;
						pressedTime = time;
						if(Pressed is not null)
						{
							Pressed(this, new(button, mouse.Position));
						}
						break;
					}
				}
			}
			else
			{
				if(!mouse.IsDown(DownButton.Value))
				{
					double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
					if(Released is not null)
					{
						Released(this, new(DownButton.Value, mouse.Position, dt));
					}
					DownButton = null;
				}
			}
		}
		else
		{
			if(Hovering)
			{
				Hovering = false;
				double dt = time.TotalGameTime.TotalMilliseconds - hoverTime.TotalGameTime.TotalMilliseconds;
				if(Unhovered is not null)
				{
					Unhovered(this, new(mouse.Position, dt));
				}
			}
			if(DownButton is not null)
			{
				double dt = time.TotalGameTime.TotalMilliseconds - pressedTime.TotalGameTime.TotalMilliseconds;
				if(Released is not null)
				{
					Released(this, new(DownButton.Value, mouse.Position, dt));
				}
				DownButton = null;
			}
		}
	}

	/// <summary>
	/// The relative position of the slider cursor.
	/// If the number of positions is positive, this will be within the range [0,positions)
	/// Otherwise this will be within the range [0,Width)
	/// </summary>
	public int Position
	{
		get => _position;
		set => MovePosition(value, true);
	}

	public override IEnumerable<IComponent> Children => [Cursor];
	public override bool HasChildren => true;
	public override bool AddChild(IComponent component) => false;
	public override bool RemoveChild(IComponent component) => false;
	public override void Invalidate()
	{
		MovePosition(_position, false);
		Cursor.Invalidate();
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(Texture, Geometry, Color);
	}

	public void Reset()
	{
		DownButton = null;
		Hovering = false;
		MovePosition(0, false);
	}

	private void MovePosition(int position, bool raise)
	{
		if(positions <= 0)
		{
			switch(direction)
			{
				case Direction.Right:
				case Direction.Left:
					if(position >= Width) position = Width - 1;
					else if(position < 0) position = 0;
						break;
				case Direction.Down:
				case Direction.Up:
					if(position >= Height) position = Height - 1;
					else if(position < 0) position = 0;
					break;
				default:
					break;
			}
			switch(direction)
			{
				case Direction.Right:
					Cursor.SetCenter(Left + position, Center.Y);
					break;
				case Direction.Left:
					Cursor.SetCenter(Right - position, Center.Y);
					break;
				case Direction.Down:
					Cursor.SetCenter(Center.X, Top + position);
					break;
				case Direction.Up:
					Cursor.SetCenter(Center.X, Bottom - position);
					break;
				default:
					throw new InvalidOperationException("Invalid Slider direction.");
			}
		}
		else
		{
			if(position >= positions) position = positions - 1;
			else if(position < 0) position = 0;

			switch(direction)
			{
				case Direction.Right:
					Cursor.SetCenter(Left + Width / positions * position, Center.Y);
					break;
				case Direction.Left:
					Cursor.SetCenter(Right - Width / positions * position, Center.Y);
					break;
				case Direction.Down:
					Cursor.SetCenter(Center.X, Top + Height / positions * position);
					break;
				case Direction.Up:
					Cursor.SetCenter(Center.X, Bottom - Height / positions * position);
					break;
				default:
					throw new InvalidOperationException("Invalid Slider direction.");
			}
		}
		if(raise && SliderMoved is not null) SliderMoved(this, new(_position, position, positions));
		_position = position;
	}

	private void MoveCursor(bool raise)
	{
		switch(direction)
		{
			case Direction.Right:
			case Direction.Left:
				MovePosition(ClosestX(_hoverPosition.Y),raise);
				break;
			case Direction.Down:
			case Direction.Up:
				MovePosition(ClosestY(_hoverPosition.Y), raise);
				break;
			default:
				throw new InvalidOperationException("Invalid Slider direction.");
		}
	}

	private int ClosestX(int x)
	{
		if(x < Left) x = Left;
		else if(x > Right) x = Right;

		int relativePosition = x - Left;
		double proportionalPosition = (double)relativePosition / (double)Width;
		int normalizedPosition = (int)Math.Round(proportionalPosition * (positions - 1));
		return Top + normalizedPosition * (Width / positions);
	}

	private int ClosestY(int y)
	{
		if(y < Top) y = Top;
		else if(y > Bottom) y = Bottom;
		
		int relativePosition = y - Top;
		double proportionalPosition = (double)relativePosition / (double)Height;
		int normalizedPosition = (int)Math.Round(proportionalPosition * (positions - 1));
		return Top + normalizedPosition * (Height / positions);
	}

	private int _position = 0;
	private Point _hoverPosition = default;
	private GameTime hoverTime = new();
	private GameTime pressedTime = new();
	private readonly List<Mouse.Buttons> buttons = [Mouse.Buttons.Left];
}
