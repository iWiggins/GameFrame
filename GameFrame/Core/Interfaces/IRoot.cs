using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Core.Interfaces;
public interface IRoot : IComponent, IDrawZone
{
	public void AddKeyboard(IComponent keyboard);

	public void AddMouse(IComponent mouse);
}
