using System;
using System.Collections.Generic;

namespace ReversePolishNotation.Interpreters
{
	public class ParenthesesInterpreter : IRpnInterpreter
	{
		public bool Interpret(string element, Stack<Operator> stackOfOperators, IList<IRpnElement> rpnOutput)
		{
			if ("()".Contains(element))
			{
				if (element.Equals("("))
					stackOfOperators.Push(new Operator() { Priority = 0 });
				else
				{
					try
					{
						while (stackOfOperators.Peek().Priority != 0)
						{
							rpnOutput.Add(stackOfOperators.Pop().RpnOperator);
						}
						stackOfOperators.Pop();
					}
					catch (InvalidOperationException)
					{
						throw new Exception("Отсутствует открывающая скобка");
					}
				}
				return true;
			}

			return false;
		}
	}
}