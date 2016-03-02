using System.Collections.Generic;

namespace ReversePolishNotation.Interpreters
{
	public class AdditionalOperatorsInterpreter : BasicOperatorsInterpreter
	{
		public AdditionalOperatorsInterpreter()
		{
			Operators = new Dictionary<string, Operator>()
			{
				{">>", new Operator() { Priority = 5, RpnOperator = new RpnBinaryOperator((x, y) => (int)x >> (int)y) }},
				{"<<", new Operator() { Priority = 5, RpnOperator = new RpnBinaryOperator((x, y) => (int)x << (int)y) }}
			};
		}
	}
}