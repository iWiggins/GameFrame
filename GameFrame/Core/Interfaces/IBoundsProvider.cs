using Microsoft.Xna.Framework;

namespace GameFrame.Core.Interfaces;
/// <summary>
/// An object capable of providing the bounds of the screen to a Frame.
/// </summary>
public interface IBoundsProvider
{
	Rectangle Bounds { get; }
}
