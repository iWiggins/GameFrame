using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Interfaces;
/// <summary>
/// A component that should be drawn using a spriteBatch.
/// </summary>
public interface IDraw
{
	/// <summary>
	/// Draw the component using the spriteBatch.
	/// </summary>
	public void Draw(SpriteBatch spriteBatch);
}
