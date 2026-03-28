using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace GameFrame.Core.Management;
/// <summary>
/// A MonoGame <see cref="Game"/> which automates game management through the use of <see cref="Frame"/> automation.
/// </summary>
/// <remarks>
/// Has an exception handler and crash logger on by default in release builds.
/// This is disabled in debug builds.
/// This can be disabled in release builds by setting <see cref="Log"/> to null.
/// </remarks>
public abstract class ManagedGame : Game
{
	public static class Defaults
	{
		public static bool Fullscreen => true;
		public static bool Borderless => true;
		public static GraphicsProfile HardwareAcceleration => GraphicsProfile.HiDef;
		public static bool VSync => true;
		public static int Width => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
		public static int Height => GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
		public static string Log => "errorlog.txt";
		public static bool MouseVisible => true;
	}

	/// <summary>
	/// Whether the game is fullscreen.
	/// </summary>
	public bool Fullscreen
	{
		get => _graphics.IsFullScreen;
		set => _graphics.IsFullScreen = value;
	}
	/// <summary>
	/// Whether the game window is borderless.
	/// </summary>
	public bool Borderless
	{
		get => !_graphics.HardwareModeSwitch;
		set => _graphics.HardwareModeSwitch = !value;
	}
	/// <summary>
	/// The Hardware Acceleration mode to use.
	/// </summary>
	public GraphicsProfile HardwarwareAcceleration
	{
		get => _graphics.GraphicsProfile;
		set => _graphics.GraphicsProfile = value;
	}
	/// <summary>
	/// Is Vertical Sync enabled.
	/// </summary>
	public bool VSync
	{
		get => _graphics.SynchronizeWithVerticalRetrace;
		set => _graphics.SynchronizeWithVerticalRetrace = value;
	}
	/// <summary>
	/// The horizontal resolution.
	/// </summary>
	public int Width
	{
		get => _graphics.PreferredBackBufferWidth;
		set => _graphics.PreferredBackBufferWidth = value;
	}
	/// <summary>
	/// The vertical resolution.
	/// </summary>
	public int Height
	{
		get => _graphics.PreferredBackBufferHeight;
		set => _graphics.PreferredBackBufferHeight = value;
	}
	/// <summary>
	/// An optional filename to use for a crash log file.
	/// </summary>
	public string? Log { get; set; }
	protected ManagedGame()
	{
		_graphics = new GraphicsDeviceManager(this)
		{
			IsFullScreen = Defaults.Fullscreen,
			HardwareModeSwitch = !Defaults.Borderless,
			GraphicsProfile = Defaults.HardwareAcceleration,
			SynchronizeWithVerticalRetrace = Defaults.VSync,
			PreferredBackBufferWidth = Defaults.Width,
			PreferredBackBufferHeight = Defaults.Height
		};
		Log = Defaults.Log;
		Content.RootDirectory = "Content";
		IsMouseVisible = Defaults.MouseVisible;
	}

	/// <summary>
	/// Get an <see cref="IBoundsProvider"/> for this game.
	/// </summary>
	protected virtual IBoundsProvider Bounds => new ScreenProvider(Window);

	/// <summary>
	/// Create the entry frame to the game. Commonly a main menu or loading screen.
	/// </summary>
	/// <remarks>
	/// This is also a good function to use for loading settings files,
	/// rather than overriding <see cref="Initialize"/> or <see cref="LoadContent"/>.
	/// </remarks>
	/// <param name="sprites">The SpriteBatch to provide to the frame.</param>
	/// <returns>The created frame.</returns>
	protected abstract Frame CreateFirstFrame(ContentManager content, SpriteBatch sprites, IBoundsProvider bounds);

	protected override void Initialize()
	{
#if !DEBUG
		try
		{
#endif
			base.Initialize();
#if !DEBUG
		}
		catch(Exception e)
		{
			HandleException(e);
		}
#endif
	}

	protected override void LoadContent()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);
#if !DEBUG
		try
		{
#endif
			_currentFrame = CreateFirstFrame(Content, _spriteBatch, Bounds);
			_currentFrame.Initialize();
			IsMouseVisible = _currentFrame.Cursor is null;
#if !DEBUG
		}
		catch(Exception e)
		{
			HandleException(e);
		}
#endif
	}

	protected override void Update(GameTime gameTime)
	{
#if !DEBUG
		try
		{
#endif
			Frame? nextFrame = _currentFrame!.Update(gameTime);

			if(nextFrame is null) Exit();

			else if(nextFrame != _currentFrame)
			{
				// If switching to an uninitialized frame, initialize it.
				// otherwise reset it.
				if(!nextFrame.Initialized) nextFrame.Initialize();
				else nextFrame.Reset();
				IsMouseVisible = nextFrame.Cursor is null;
				_currentFrame = nextFrame;
			}
			base.Update(gameTime);
#if !DEBUG
		}
		catch(Exception e)
		{
			HandleException(e);
		}
#endif

	}

	protected override void Draw(GameTime gameTime)
	{
#if !DEBUG
		try
		{
#endif
			GraphicsDevice.Clear(Color.Black);

			_currentFrame?.Draw();

			base.Draw(gameTime);
#if !DEBUG
		}
		catch(Exception e)
		{
			HandleException(e);
		}
#endif
	}

	/// <summary>
	/// Handles most exceptions by writing a log file with the exception details,
	/// then exiting the game.
	/// </summary>
	/// <param name="e">The caught exception.</param>
	protected virtual void HandleException(Exception e)
	{
		if(Log is not null)
		{
			using StreamWriter writer = new(Log);
			writer.WriteLine(e.Message);
			writer.WriteLine();
			writer.Write(e.StackTrace);
		}
		Exit();
	}

	protected GraphicsDeviceManager _graphics;
	protected SpriteBatch? _spriteBatch;
	protected Frame? _currentFrame;
}