using Microsoft.Xna.Framework;

namespace GameFrame.Core.Interfaces;
/// <summary>
/// Components with update logic.
/// </summary>
public interface IUpdate
{
	/// <summary>
	/// Update the component.
	/// </summary>
	/// <param name="time">The time since the last update.</param>
	void Update(GameTime time);
}
