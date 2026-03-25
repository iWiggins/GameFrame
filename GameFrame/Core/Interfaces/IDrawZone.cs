using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Interfaces;
public interface IDrawZone
{
	void StartDrawing(SpriteBatch spriteBatch);
	void EndDrawing(SpriteBatch spriteBatch);
}
