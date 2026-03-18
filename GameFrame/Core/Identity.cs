namespace GameFrame.Core;
internal static class Identity
{
	private static ulong _id = 0;

	/// <summary>
	/// Generates the next ID in the sequence.
	/// </summary>
	/// <returns>A unique integer ID.</returns>
	public static ulong GenerateId() => _id++;
}
