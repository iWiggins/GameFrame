using GameFrame.Core.Components;
using GameFrame.Core.Geometrics;
using GameFrame.Core.Interfaces;
using GameFrame.Layout;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameFrame.Components;
/// <summary>
/// An image that scales its texture instead of stretching,
/// to avoid distorting the image.
/// </summary>
/// <param name="texture"><inheritdoc cref="FramedImage.Texture" path="/summary"/></param>
/// <param name="parent"><inheritdoc cref="Component.Component" path="/param[@name='parent']"/></param>
/// <param name="layer"><inheritdoc cref="Component.Component" path="/param[@name='layer']"/></param>
public class FramedImage(Texture2D texture, IComponent? parent = null, int layer = 0) :
	GeometricTwig<ScaleLayout>(new(), parent, layer), IInitialize
{
	public bool Initialized { get; private set; } = false;

	/// <summary>
	/// <inheritdoc cref="Image.Texture" path="/summary"/>
	/// </summary>
	public Texture2D Texture
	{
		get => _image.Texture;
		set => _image.Texture = value;
	}
	/// <summary>
	/// <inheritdoc cref="Image.Color" path="/summary"/>
	/// </summary>
	public Color Color
	{
		get => _image.Color;
		set => _image.Color = value;
	}

	public void Initialize()
	{
		_image.Width = Texture.Width;
		_image.Height = Texture.Height;
		Child.AddChild(_image);
	}
	private readonly Image _image = new(texture);
}
