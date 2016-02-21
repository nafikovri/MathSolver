using System.Collections.Generic;

namespace ReversePolishNotation
{
	public interface IRpnTranslator
	{
		List<RpnElement> Translate(string expression);
	}
}