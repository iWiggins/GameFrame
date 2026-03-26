using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;

namespace GameFrame.Core;
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

	public bool Fullscreen
	{
		get => _graphics.IsFullScreen;
		set => _graphics.IsFullScreen = value;
	}
	public bool Borderless
	{
		get => !_graphics.HardwareModeSwitch;
		set => _graphics.HardwareModeSwitch = !value;
	}
	public GraphicsProfile HardwarwareAcceleration
	{
		get => _graphics.GraphicsProfile;
		set => _graphics.GraphicsProfile = value;
	}
	public bool VSync
	{
		get => _graphics.SynchronizeWithVerticalRetrace;
		set => _graphics.SynchronizeWithVerticalRetrace = value;
	}
	public int Width
	{
		get => _graphics.PreferredBackBufferWidth;
		set => _graphics.PreferredBackBufferWidth = value;
	}
	public int Height
	{
		get => _graphics.PreferredBackBufferHeight;
		set => _graphics.PreferredBackBufferHeight = value;
	}
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
		IsMouseVisible = true;
	}

	/// <summary>
	/// Create the entry frame to the game. Commonly a main menu or loading screen.
	/// </summary>
	/// <remarks>
	/// This is also a good function to use for loading settings files,
	/// rather than overriding Initialize or LoadContent.
	/// </remarks>
	/// <param name="sprites">The SpriteBatch to provide to the frame.</param>
	/// <returns>The created frame.</returns>
	protected abstract Frame CreateFirstFrame(SpriteBatch sprites);

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
			_currentFrame = CreateFirstFrame(_spriteBatch);
			_currentFrame.Initialize();
			IsMouseVisible = _currentFrame.Cursor is not null;
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
				nextFrame.Initialize();
				IsMouseVisible = nextFrame.Cursor is not null;
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

			_currentFrame!.Draw();

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