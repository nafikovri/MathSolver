using System.Collections.Generic;

namespace ReversePolishNotation
{
	public interface IRpnTranslator
	{
		IEnumerable<IRpnElement> Translate(string expression);
	}

}