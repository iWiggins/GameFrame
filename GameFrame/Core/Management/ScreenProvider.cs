using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Management;
public class ScreenProvider(GameWindow window) : IBoundsProvider
{
	public Rectangle Bounds => window.ClientBounds;
}
