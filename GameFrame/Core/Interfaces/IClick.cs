using GameFrame.Core.EventHandlers;
using Microsoft.Xna.Framework;
using GameFrame.Core.Input;

namespace GameFrame.Core.Interfaces;
public interface IClick
{
	event MouseDownHandler? Pressed;
	event MouseUpHandler? Released;
	event MouseHoverHandler? Hovered;
	event MouseUnhoverHandler? Unhovered;
	bool Down { get; }
	bool Hovering { get; }
	void Click(GameTime time, Mouse mouse);
}
