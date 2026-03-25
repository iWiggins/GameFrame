using GameFrame.Core.Interfaces;
using System.Collections.Generic;

namespace GameFrame.Layout;

/// <summary>
/// A layout which centers all components it contains.
/// </summary>
public class CenterLayout(IComponent? parent = null) : Layout(parent)
{
    protected override IEnumerable<IComponent> Arrange()
    {
        foreach(var child in Children){
            if(child is IGeometric geometric)
            {
                geometric.SetCenter(Center);
            }
        }

        return Order();
    }
}