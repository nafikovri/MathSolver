using System.Collections.Generic;

namespace ReversePolishNotation
{
	public interface ISplitter
	{
		IEnumerable<string> Split(string expression);
	}
}