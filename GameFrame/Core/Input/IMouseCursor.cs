using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Core.Input;
public interface IMouseCursor : IComponent, IDraw
{
	bool Centered { get; set; }
	Color Color { get; set; }
	Point Position { get; }
	Texture2D Texture { get; set; }
	void Move(int x, int y);
	void Move(Point p);
	void Move(Vector2 p);
}