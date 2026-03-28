using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Core.Components;
/// <summary>
/// This is the default <see cref="IRoot"/> used by frames
/// if none is provided by the implementor.
/// It uses minimal drawing settings.
/// </summary>
public class Root : Branch, IRoot
{
	public SpriteSortMode DefaultSortMode { get; set; } = SpriteSortMode.Deferred;
	public BlendState? DefaultBlendState { get; set; } = null;
	public SamplerState? DefaultSamplerState { get; set; } = null;
	public DepthStencilState? DefaultStencilState { get; set; } = null;
	public RasterizerState? DefaultRasterizerState { get; set; } = null;

	public void AddKeyboard(IComponent keyboard) => AddChild(keyboard);
	public void AddMouse(IComponent mouse) => AddChild(mouse);

	public void StartDrawing(SpriteBatch spriteBatch)
	{
		spriteBatch.Begin(DefaultSortMode, DefaultBlendState, DefaultSamplerState, DefaultStencilState, DefaultRasterizerState, null, null);
	}
	public void EndDrawing(SpriteBatch spriteBatch)
	{
		spriteBatch.End();
	}
}
