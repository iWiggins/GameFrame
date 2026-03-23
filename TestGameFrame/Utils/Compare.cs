using Microsoft.Xna.Framework;

namespace TestGameFrame.Utils;
internal class Compare
{
	public static bool Same(Rectangle r1, Rectangle r2)
	{
		return
			r1.X == r2.X &&
			r1.Y == r2.Y &&
			r1.Width == r2.Width &&
			r1.Height == r2.Height;
	}
}
