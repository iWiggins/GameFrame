using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Geometrics;
internal static class RectangleExtensions
{
	public static void SetCenter(this Rectangle rect, int x, int y)
	{
		rect.X = x - rect.Width / 2;
		rect.Y = y - rect.Height / 2;
		
	}

	public static void SetCenter(this Rectangle rect, Point center)
	{
		rect.X = center.X - rect.Width / 2;
		rect.Y = center.Y - rect.Height / 2;

	}
}
