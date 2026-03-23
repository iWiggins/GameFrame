using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGameFrame.Utils;
internal static class ListExtensions
{
	public static void Shuffle<T>(this List<T> list, Random? rand = null)
	{
		rand ??= new();

		for(int i = 0; i < list.Count-1; ++i)
		{
			int j = rand.Next(i, list.Count);
			T tmp = list[i];
			list[i] = list[j];
			list[j] = tmp;
		}
	}
}
