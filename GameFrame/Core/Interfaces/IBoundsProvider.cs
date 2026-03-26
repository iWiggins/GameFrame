using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Interfaces;
public interface IBoundsProvider
{
	Rectangle Bounds { get; }
}
