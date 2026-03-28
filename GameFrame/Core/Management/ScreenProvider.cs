using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Management;
/// <summary>
/// A simple bounds provider passing information from the <see cref="GameWindow"/> member of <see cref="Game"/>.
/// </summary>
/// <param name="window">The game window of the game this is initialized from.</param>
public class ScreenProvider(GameWindow window) : IBoundsProvider
{
	public Rectangle Bounds => window.ClientBounds;
}
