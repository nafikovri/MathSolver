using System.Collections.Generic;

namespace ReversePolishNotation.Interpreters
{
	public class BasicOperatorsInterpreter : IRpnInterpreter
	{
		protected Dictionary<string, Operator> Operators;

		public BasicOperatorsInterpreter()
		{
			Operators = new Dictionary<string, Operator>()
			{
				{"*", new Operator() {Priority = 2, RpnOperator = new RpnBinaryOperator(BasicOperators.Multiplication)}},
				{"/", new Operator() {Priority = 2, RpnOperator = new RpnBinaryOperator(BasicOperators.Division)}},
				{"+", new Operator() {Priority = 1, RpnOperator = new RpnBinaryOperator(BasicOperators.Addition)}},
				{"-", new Operator() {Priority = 1, RpnOperator = new RpnBinaryOperator(BasicOperators.Subtraction)}}
			};
		}

		public bool Interpret(string element, Stack<Operator> stackOfOperators, IList<IRpnElement> rpnOutput)
		{
			if (Operators.ContainsKey(element))
			{
				var oper = Operators[element];

				while (stackOfOperators.Count != 0 &&
					oper.Priority <= stackOfOperators.Peek().Priority)
				{
					rpnOutput.Add(stackOfOperators.Pop().RpnOperator);
				}

				stackOfOperators.Push(oper);
				return true;
			}

			return false;
		}
	}
}