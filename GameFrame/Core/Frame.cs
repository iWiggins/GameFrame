using GameFrame.Core.Components;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core;
public abstract class Frame
{
	protected RootComponent Root { get; private set; }
	protected Keyboard Keyboard { get; }
	protected Mouse Mouse { get; }
	
	public Frame(SpriteBatch spriteBatch)
	{
		this.spriteBatch = spriteBatch;

		Root = new();
		Keyboard = new(Root);
		Mouse = new(Root);
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
		bool drawing = false;

		void DrawComponent(IComponent component)
		{
			if(component.Enabled)
			{
				if(component is IDrawZone dzone)
				{
					if(drawing)
					{
						spriteBatch.End();
					}
					dzone.StartDrawing(spriteBatch);
					drawing = true;
					if(component is IDraw draw)
					{
						draw.Draw(spriteBatch);
					}
					foreach(IComponent child in component.Children)
					{
						DrawComponent(child);
					}
					if(drawing)
					{
						spriteBatch.End();
						drawing = false;
					}
				}
				else
				{
					if(!drawing)
					{
						Root.StartDrawing(spriteBatch);
						drawing = true;
						if(component is IDraw draw)
						{
							draw.Draw(spriteBatch);
						}
						foreach(IComponent child in component.Children)
						{
							DrawComponent(child);
						}
					}
				}
			}
		}

		PreDraw();

		DrawComponent(Root);

		// This shouldn't happen, because the root is a draw zone.
		// But it consts nearly nothing to check.
		if(drawing) spriteBatch.End();

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
	private readonly SpriteBatch spriteBatch;
}
