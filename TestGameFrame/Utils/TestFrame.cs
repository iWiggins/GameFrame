using GameFrame.Core;
using GameFrame.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace TestGameFrame.Utils;
// The null constructor values do not matter, as those values are not used in testing.
internal class TestFrame(TestRoot root) : Frame(null!, null!, root)
{
	protected override Frame? PostUpdate(GameTime time) => this;

	public IEnumerable<IComponent> InitializeQueue => _initializeQueue;

	public void AddInitialize(IComponent component) => _initializeQueue.Add(component);

	public void ClearInitializations() => _initializeQueue.Clear();

	public IEnumerable<IComponent> UpdateQueue => _updateQueue;

	public void AddUpdate(IComponent component) => _updateQueue.Add(component);

	public void ClearUpdates() => _updateQueue.Clear();

	public IEnumerable<IComponent> DrawQueue => _drawQueue;

	public void AddDraw(IComponent component) => _drawQueue.Add(component);

	public void ClearDraws() => _drawQueue.Clear();



	private readonly List<IComponent> _initializeQueue = [];
	private readonly List<IComponent> _updateQueue = [];
	private readonly List<IComponent> _drawQueue = [];
}
