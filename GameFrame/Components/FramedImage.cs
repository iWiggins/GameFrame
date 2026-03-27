using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components;
public class FramedImage(Texture2D texture, IComponent? parent = null, int layer = 0) :
	GeometricTwig<ScaleLayout>(new(), parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;

	public void Initialize() => Child.AddChild(new Image(texture));
}
