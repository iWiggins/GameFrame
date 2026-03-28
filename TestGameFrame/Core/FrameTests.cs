using GameFrame.Core.Interfaces;
using TestGameFrame.Utils;

namespace TestGameFrame.Core;
public class FrameTests
{
	[Fact]
	public void InitializationsHappenPreorder()
	{
		TestRoot root = new(0);
		TestFrame frame = new(root);

		TestInitializeBranch parent = new(1, frame.AddInitialize);
		root.AddChild(parent);

		TestInitializeBranch leftChild = new(2, frame.AddInitialize);
		parent.AddChild(leftChild);

		TestInitializeBranch rightChild = new(3, frame.AddInitialize);
		parent.AddChild(rightChild);

		TestInitializeLeaf llGrandchild = new(4, frame.AddInitialize);
		leftChild.AddChild(llGrandchild);

		TestInitializeLeaf lrGrandchild = new(5, frame.AddInitialize);
		leftChild.AddChild(lrGrandchild);

		TestInitializeLeaf rlGrandchild = new(6, frame.AddInitialize);
		rightChild.AddChild(rlGrandchild);

		TestInitializeLeaf rrGrandchild = new(7, frame.AddInitialize);
		rightChild.AddChild(rrGrandchild);

		ulong[] expected = [1, 2, 4, 5, 3, 6, 7];

		frame.Initialize();

		Assert.True(parent.Initialized);
		Assert.True(leftChild.Initialized);
		Assert.True(rightChild.Initialized);
		Assert.True(llGrandchild.Initialized);
		Assert.True(lrGrandchild.Initialized);
		Assert.True(rlGrandchild.Initialized);
		Assert.True(rrGrandchild.Initialized);

		var actual = frame.InitializeQueue;

		var comparison = expected.Zip(actual);

		int count = 0;
		foreach((ulong e, IComponent c) in comparison)
		{
			Assert.Equal(e, c.Id);
			++count;
		}

		Assert.Equal(expected.Length, count);
	}

	[Fact]
	public void DrawsHappenPreorder()
	{
		TestRoot root = new(0);
		TestFrame frame = new(root);

		TestDrawBranch parent = new(1, frame.AddDraw);
		root.AddChild(parent);

		TestDrawBranch leftChild = new(2, frame.AddDraw);
		parent.AddChild(leftChild);

		TestDrawBranch rightChild = new(3, frame.AddDraw);
		parent.AddChild(rightChild);

		TestDrawLeaf llGrandchild = new(4, frame.AddDraw);
		leftChild.AddChild(llGrandchild);

		TestDrawLeaf lrGrandchild = new(5, frame.AddDraw);
		leftChild.AddChild(lrGrandchild);

		TestDrawLeaf rlGrandchild = new(6, frame.AddDraw);
		rightChild.AddChild(rlGrandchild);

		TestDrawLeaf rrGrandchild = new(7, frame.AddDraw);
		rightChild.AddChild(rrGrandchild);

		ulong[] expected = [1, 2, 4, 5, 3, 6, 7];

		frame.Draw();

		Assert.True(parent.Drawn);
		Assert.True(leftChild.Drawn);
		Assert.True(rightChild.Drawn);
		Assert.True(llGrandchild.Drawn);
		Assert.True(lrGrandchild.Drawn);
		Assert.True(rlGrandchild.Drawn);
		Assert.True(rrGrandchild.Drawn);

		var actual = frame.DrawQueue;

		var comparison = expected.Zip(actual);

		int count = 0;
		foreach((ulong e, IComponent c) in comparison)
		{
			Assert.Equal(e, c.Id);
			++count;
		}

		Assert.Equal(expected.Length, count);
	}

	[Fact]
	public void UpdatesHappenPreorder()
	{
		TestRoot root = new(0);
		TestFrame frame = new(root);

		TestUpdateBranch parent = new(1, frame.AddUpdate);
		root.AddChild(parent);

		TestUpdateBranch leftChild = new(2, frame.AddUpdate);
		parent.AddChild(leftChild);

		TestUpdateBranch rightChild = new(3, frame.AddUpdate);
		parent.AddChild(rightChild);

		TestUpdateLeaf llGrandchild = new(4, frame.AddUpdate);
		leftChild.AddChild(llGrandchild);

		TestUpdateLeaf lrGrandchild = new(5, frame.AddUpdate);
		leftChild.AddChild(lrGrandchild);

		TestUpdateLeaf rlGrandchild = new(6, frame.AddUpdate);
		rightChild.AddChild(rlGrandchild);

		TestUpdateLeaf rrGrandchild = new(7, frame.AddUpdate);
		rightChild.AddChild(rrGrandchild);

		ulong[] expected = [1, 2, 4, 5, 3, 6, 7];

		frame.Update(new());

		Assert.True(parent.Updated);
		Assert.True(leftChild.Updated);
		Assert.True(rightChild.Updated);
		Assert.True(llGrandchild.Updated);
		Assert.True(lrGrandchild.Updated);
		Assert.True(rlGrandchild.Updated);
		Assert.True(rrGrandchild.Updated);

		var actual = frame.UpdateQueue;

		var comparison = expected.Zip(actual);

		int count = 0;
		foreach((ulong e, IComponent c) in comparison)
		{
			Assert.Equal(e, c.Id);
			++count;
		}

		Assert.Equal(expected.Length, count);
	}
}
