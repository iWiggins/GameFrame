using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Core.Interfaces;
/// <summary>
/// A component which makes a new call to <see cref="SpriteBatch.Begin"/>.
/// This is commonly used to change drawing modes over a part of the screen, but not the entire screen.
/// Or to add shaders to a component or family of components.
/// </summary>
public interface IDrawZone
{
	/// <summary>
	/// Begin drawing by calling <see cref="SpriteBatch.Begin"/> with whatever custom logic is needed.
	/// </summary>
	/// <param name="spriteBatch">The spritebatch to draw with.</param>
	void StartDrawing(SpriteBatch spriteBatch);

	/// <summary>
	/// Ends drawing by calling <see cref="SpriteBatch.End"/> and performing any needed cleanup logic.
	/// </summary>
	/// <param name="spriteBatch">THe spritebatch to end drawing.</param>
	void EndDrawing(SpriteBatch spriteBatch);
}
