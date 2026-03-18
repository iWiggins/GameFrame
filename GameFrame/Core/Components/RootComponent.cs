using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Core.Components;
public class RootComponent : Branch, IDrawZone
{
	public SpriteSortMode DefaultSortMode { get; set; } = SpriteSortMode.Deferred;
	public BlendState? DefaultBlendState { get; set; } = null;
	public SamplerState? DefaultSamplerState { get; set; } = null;
	public DepthStencilState? DefaultStencilState { get; set; } = null;
	public RasterizerState? DefaultRasterizerState { get; set; } = null;

	public void StartDrawing(SpriteBatch spriteBatch)
	{
		spriteBatch.Begin(DefaultSortMode, DefaultBlendState, DefaultSamplerState, DefaultStencilState, DefaultRasterizerState, null, null);
	}
}
