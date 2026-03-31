using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrame.Components.Text;
internal static class StringSupport
{
	public static IEnumerable<string> SplitLines(string data)
	{
		StringBuilder line = new();

		bool trailingNewline  = line.Length > 0 && line[^1] == '\n';

		int pos = 0;
		while(pos < data.Length)
		{
			char c = data[pos];
			if(c == '\n')
			{
				yield return line.ToString();
				line.Clear();
				++pos;
				continue;
			}
			else if(c == '\r')
			{
				if(pos+1 < data.Length)
				{
					++pos;
				}
				yield return line.ToString();
				line = line.Clear();
				++pos;
				continue;
			}
			line.Append(c);
			++pos;
		}
		yield return line.ToString();
		if(trailingNewline) yield return "";
	}
}
