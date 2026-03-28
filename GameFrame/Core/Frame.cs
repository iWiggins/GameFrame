using GameFrame.Core.Components;
using GameFrame.Core.Input;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameFrame.Core;
/// <summary>
/// The core automation logic for a GameFrame game.
/// Consists of four stages:
/// <list type="number">
/// <item>One time, <see cref="Initialize"/> triggers <see cref="IInitialize.Initialize"/> on implementing components.</item>
/// <item>Each update, <see cref="Update"/> triggers <see cref="IUpdate.Update"/> on implementing components.</item>
/// <item>Each draw, <see cref="Draw"/> triggers <see cref="IDraw.Draw"/> on implementing components.</item>
/// <item>On changing back to this Frame, <see cref="Reset"/> triggers <see cref="IReset.Reset"/> on implementing components.</item>
/// </list>
/// Any <see cref="IComponent"/> implementing one of the above interfaces
/// that is an ancestor of the Frame's <see cref="Frame.Root"/> will have its functionality automated.
/// </summary>
public abstract class Frame
{
	/// <summary>
	/// The root component of the frame. All components should be ancestors of this component.
	/// </summary>
	protected IRoot Root { get; private set; }

	/// <summary>
	/// A provider for the screen dimensions.
	/// </summary>
	protected IBoundsProvider Screen { get; private set; }

	/// <summary>
	/// A manager for the keyboard.
	/// </summary>
	protected Keyboard Keyboard { get; }

	/// <summary>
	/// A manager for the mouse.
	/// </summary>
	protected Mouse Mouse { get; }

	/// <summary>
	/// The spriteBatch used for drawing.
	/// </summary>
	protected readonly SpriteBatch SpriteBatch;

	/// <summary>
	/// An optional mouse cursor.
	/// </summary>
	public IMouseCursor? Cursor
	{
		get => Mouse.Cursor;
		set => Mouse.Cursor = value;
	}

	/// <summary>
	/// Whether the frame has been initialized.
	/// </summary>
	public bool Initialized { get; private set; }

	/// <summary>
	/// <inheritdoc cref="Frame(SpriteBatch, IBoundsProvider, IRoot)"/>
	/// </summary>
	/// <param name="spriteBatch"><inheritdoc cref="Frame(SpriteBatch, IBoundsProvider, IRoot)" path="/param[@name='spriteBatch']"/></param>
	/// <param name="bounds"><inheritdoc cref="Frame(SpriteBatch, IBoundsProvider, IRoot)" path="/param[@name='bounds']"/></param>
	public Frame(SpriteBatch spriteBatch, IBoundsProvider bounds) :
		this(spriteBatch, bounds, new Root())
		{}

	/// <summary>
	/// Create a frame.
	/// </summary>
	/// <param name="spriteBatch">The spritebatch for drawing.</param>
	/// <param name="bounds">The bounds provider for the screen dimensions.</param>
	/// <param name="root">A root component.</param>
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
		_drawZones = [];
	}

	/// <summary>
	/// Optional logic before Frame begins initializing the components.
	/// </summary>
	protected virtual void PreInitialize() { }

	/// <summary>
	/// Performs pre-initialization logic,
	/// initializes all uninitialized <see cref="IInitialize"/> components,
	/// and performs post-initialization logic.
	/// </summary>
	/// <remarks>
	/// This <b>will</b> call <see cref="IInitialize.Initialize"/> on components
	/// where <see cref="IComponent.Enabled"/> is false.
	/// </remarks>
	public void Initialize()
	{
		static void InitializeComponent(IComponent component)
		{
			if(component is IInitialize initialize)
			{
				if(!initialize.Initialized) initialize.Initialize();
			}
			foreach(IComponent child in component.Children)
			{
				InitializeComponent(child);
			}
		}

		PreInitialize();
		InitializeComponent(Root);
		PostInitialize();
		Initialized = true;
	}

	/// <summary>
	/// Optional logic after the Frame initializes all of the components.
	/// </summary>
	protected virtual void PostInitialize() { }

	/// <summary>
	/// Optional logic before the Frame updates all of the components.
	/// </summary>
	/// <param name="time"><inheritdoc cref="Update" path="/param[@name='time']"/></param>
	protected virtual void PreUpdate(GameTime time) { }

	/// <summary>
	/// Performs pre-update logic.
	/// Updates all <see cref="IUpdate"/> components.
	/// Raises click events on <see cref="IClick"/> components and <see cref="Mouse"/>.
	/// Raises key events on <see cref="Keyboard"/>.
	/// Performs post-update logic.
	/// </summary>
	/// <remarks>
	/// This will skil any components where <see cref="IComponent.Enabled"/> is false.
	/// It will also skip the children of any such components.
	/// This will exit prematurely if <see cref="Exit"/> is called during the update process.
	/// </remarks>
	/// <param name="time">The time since the last update.</param>
	/// <returns>
	/// The frame to change to, or null to exit the game.
	/// If no change is being triggered, returns current frame.
	/// </returns>
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

	/// <summary>
	/// Optional logic after the Frame updates all components.
	/// </summary>
	/// <param name="time"><inheritdoc cref="Update" path="/param[@name='time']"/></param>
	/// <returns><inheritdoc cref="Update" path="/returns"/></returns>
	protected virtual Frame? PostUpdate(GameTime time) => this;

	/// <summary>
	/// Optional logic before the Frame draws all components.
	/// </summary>
	protected virtual void PreDraw() { }

	/// <summary>
	/// Performs pre-draw logic.
	/// Draws all <see cref="IDraw"/> components,
	/// applying <see cref="IDrawZone"/> logic.
	/// Performs post-draw logic.
	/// </summary>
	/// <remarks>
	/// This will skil any components where <see cref="IComponent.Enabled"/> is false.
	/// It will also skip the children of any such components.
	/// </remarks>
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

	/// <summary>
	/// Optional logic after the Frame draws all components.
	/// </summary>
	protected virtual void PostDraw() { }

	/// <summary>
	/// Optional logic before the Frame resets all components.
	/// </summary>
	protected virtual void PreReset() { }

	/// <summary>
	/// Performs pre-reset logic.
	/// Resets all <see cref="IReset"/> components.
	/// Performs post-reset logic.
	/// </summary>
	/// <remarks>
	/// This <b>will</b> call <see cref="IReset.Reset"/> on components where <see cref="IComponent.Enabled"/> is false.
	/// </remarks>
	public void Reset()
	{
		_exitFrame = null;
		_shouldExit = false;
		static void ResetComponent(IComponent component)
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

	/// <summary>
	/// Optional logic after the frame resets all components.
	/// </summary>
	protected virtual void PostReset() { }

	/// <summary>
	/// When called, signals that the frame should exit at the earliest opportunity.
	/// This may be during an update process.
	/// </summary>
	/// <param name="frame">The frame to switch to. If null, exits the game.</param>
	protected void Exit(Frame? frame)
	{
		_exitFrame = frame;
		_shouldExit = true;
	}

	private Frame? _exitFrame;
	private bool _shouldExit;
	private readonly Stack<IDrawZone> _drawZones;
}
