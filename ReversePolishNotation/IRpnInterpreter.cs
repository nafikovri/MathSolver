
using System.Collections.Generic;

namespace ReversePolishNotation
{
	public interface IRpnInterpreter
	{
		bool Interpret(string element, Stack<Operator> stackOfOperators, IList<IRpnElement> rpnOutput);
	}
}
