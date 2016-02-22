using System.Collections.Generic;

namespace ReversePolishNotation
{
	public interface IRpnElement
	{
		void Calculate(ref Stack<double> stack);
	}
}