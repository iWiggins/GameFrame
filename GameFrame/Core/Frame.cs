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
	protected Keyboard Keyboard { get; }
	protected Mouse Mouse { get; }
	
	public Frame(SpriteBatch spriteBatch):
		this(spriteBatch, new Root())
		{}

	public Frame(SpriteBatch spriteBatch, IRoot root)
	{
		this._spriteBatch = spriteBatch;

		Root = root;
		Keyboard = new(Root);
		Mouse = new(Root);
		root.AddKeyboard(Keyboard);
		root.AddMouse(Mouse);
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

	protected abstract Frame? PostUpdate(GameTime time);

	protected virtual void PreDraw() { }

	public void Draw()
	{
		void DrawComponent(IComponent component)
		{
			if(component.Enabled)
			{
				if(component is IDrawZone dzone)
				{
					if(_drawZones.Count > 0)
					{
						_drawZones.Peek().EndDrawing(_spriteBatch);
					}
					_drawZones.Push(dzone);
					dzone.StartDrawing(_spriteBatch);
					if(component is IDraw draw)
					{
						draw.Draw(_spriteBatch);
					}
					foreach(IComponent child in component.Children)
					{
						DrawComponent(child);
					}
					_drawZones.Pop();
					dzone.EndDrawing(_spriteBatch);
					if(_drawZones.Count > 0)
					{
						_drawZones.Peek().StartDrawing(_spriteBatch);
					}
				}
				else
				{
					if(component is IDraw draw)
					{
						draw.Draw(_spriteBatch);
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

	protected void Exit(Frame? frame)
	{
		_exitFrame = frame;
		_shouldExit = true;
	}

	private Frame? _exitFrame;
	private bool _shouldExit;
	private readonly SpriteBatch _spriteBatch;
	private readonly Stack<IDrawZone> _drawZones = [];
}
