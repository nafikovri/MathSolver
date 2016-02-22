using System.Collections.Generic;

namespace ReversePolishNotation
{
	public interface IRpnTranslator
	{
		List<IRpnElement> Translate(string expression);
	}

}