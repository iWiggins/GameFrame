using GameFrame.Core.Components;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameFrame.Core;
public abstract class Frame
{
	protected IRoot Root { get; private set; }
	protected IBoundsProvider Screen { get; private set; }
	protected Keyboard Keyboard { get; }
	protected Mouse Mouse { get; }
	protected readonly SpriteBatch SpriteBatch;

	public IMouseCursor? Cursor
	{
		get => Mouse.Cursor;
		set => Mouse.Cursor = value;
	}

	public bool Initialized { get; private set; }
	
	public Frame(SpriteBatch spriteBatch, IBoundsProvider bounds) :
		this(spriteBatch, bounds, new Root())
		{}

	public Frame(SpriteBatch spriteBatch, IBoundsProvider bounds, IRoot root)
	{
		SpriteBatch = spriteBatch;

		Screen = bounds;
		Root = root;
		Keyboard = new(Root);
		Mouse = new(Root);
		root.AddKeyboard(Keyboard);
		root.AddMouse(Mouse);
		Initialized = false;
		_exitFrame = null;
		_shouldExit = false;
	}

	protected virtual void PreInitialize() { }

	public void Initialize()
	{
		void InitializeComponent(IComponent component)
		{
			if(component.Enabled)
			{
				if(component is IInitialize initialize)
				{
					initialize.Initialize();
				}
				foreach(IComponent child in component.Children)
				{
					InitializeComponent(child);
				}
			}
		}

		PreInitialize();
		InitializeComponent(Root);
		PostInitialize();
		Initialized = true;
	}

	protected virtual void PostInitialize() { }

	protected virtual void PreUpdate(GameTime time) { }

	/// <summary>
	/// Updates all updatable components, and raises click events.
	/// </summary>
	/// <param name="time">The time since the last update.</param>
	/// <returns>The frame to change to, or null to exit the game.</returns>
	public Frame? Update(GameTime time)
	{
		void UpdateComponent(IComponent component)
		{
			if(component.Enabled)
			{
				if(component is IClick click)
				{
					click.Click(time, Mouse);
				}
				if(component is IUpdate update)
				{
					update.Update(time);
					if(_shouldExit) return;
				}
				foreach(IComponent child in component.Children)
				{
					UpdateComponent(child);
					if(_shouldExit) return;
				}
			}
		}

		if(_shouldExit) return _exitFrame;

		PreUpdate(time);

		if(_shouldExit) return _exitFrame;

		UpdateComponent(Root);
		if(_shouldExit) return _exitFrame;

		Frame? updateFrame = PostUpdate(time);
		if(_shouldExit) return _exitFrame;
		else return updateFrame;
	}

	protected virtual Frame? PostUpdate(GameTime time) => this;

	protected virtual void PreDraw() { }

	public void Draw()
	{
		void DrawComponent(IComponent component)
		{
			// The keyboard has many children that will never draw, skip it.
			if(component.Enabled && component != Keyboard)
			{
				if(component is IDrawZone dzone)
				{
					if(_drawZones.Count > 0)
					{
						_drawZones.Peek().EndDrawing(SpriteBatch);
					}
					_drawZones.Push(dzone);
					dzone.StartDrawing(SpriteBatch);
					if(component is IDraw draw)
					{
						draw.Draw(SpriteBatch);
					}
					foreach(IComponent child in component.Children)
					{
						DrawComponent(child);
					}
					_drawZones.Pop();
					dzone.EndDrawing(SpriteBatch);
					if(_drawZones.Count > 0)
					{
						_drawZones.Peek().StartDrawing(SpriteBatch);
					}
				}
				else
				{
					if(component is IDraw draw)
					{
						draw.Draw(SpriteBatch);
					}
					foreach(IComponent child in component.Children)
					{
						DrawComponent(child);
					}
				}
			}
		}

		PreDraw();

		DrawComponent(Root);

		PostDraw();
	}

	protected virtual void PostDraw() { }

	protected virtual void PreReset() { }

	public void Reset()
	{
		_exitFrame = null;
		_shouldExit = false;
		void ResetComponent(IComponent component)
		{
			if(component is IReset reset)
			{
				reset.Reset();
			}
			foreach(var child in component.Children)
			{
				ResetComponent(child);
			}
		}
		PreReset();
		ResetComponent(Root);
		PostReset();
	}

	protected virtual void PostReset() { }

	protected void Exit(Frame? frame)
	{
		_exitFrame = frame;
		_shouldExit = true;
	}

	private Frame? _exitFrame;
	private bool _shouldExit;
	private readonly Stack<IDrawZone> _drawZones = [];
}
