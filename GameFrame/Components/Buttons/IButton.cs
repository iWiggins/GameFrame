using GameFrame.Core.Interfaces;

namespace GameFrame.Components.Buttons;
/// <summary>
/// An interface for buttons. Buttons are geometric components with click logic.
/// </summary>
public interface IButton: IComponent, IClick, IGeometric
{
}
