using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components;
public class FramedImage(Texture2D texture, IComponent? parent = null, int layer = 0) :
	GeometricTwig<ScaleLayout>(new(), parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;

	public Texture2D Texture
	{
		get => _image.Texture;
		set => _image.Texture = value;
	}
	public Color Color
	{
		get => _image.Color;
		set => _image.Color = value;
	}

	public void Initialize() => Child.AddChild(_image);
	private readonly Image _image = new(texture);
}
