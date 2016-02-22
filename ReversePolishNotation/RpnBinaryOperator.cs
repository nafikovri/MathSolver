using System;
using System.Collections.Generic;

namespace ReversePolishNotation
{
	public class RpnBinaryOperator : IRpnElement
	{
		private Func<double, double, double> _operation;

		public RpnBinaryOperator(Func<double, double, double> operation)
		{
			this._operation = operation;
		}

		public void Calculate(ref Stack<double> stack)
		{
			if (stack.Count < 2)
				throw new InvalidOperationException(
					$"Недостаточно операндов для выполнения операции. Необходимо {2}, обнаружено {stack.Count}");

			var operand1 = stack.Pop();
			var operand2 = stack.Pop();

			stack.Push(this._operation(operand1, operand2));
		}
	}
}