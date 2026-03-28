using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Interfaces;
/// <summary>
/// A root component of a frame. A root component must, at the minimum, accept children and be a <see cref="IDrawZone"/>.
/// </summary>
public interface IRoot : IComponent, IDrawZone
{
	public void AddKeyboard(IComponent keyboard);

	public void AddMouse(IComponent mouse);
}
