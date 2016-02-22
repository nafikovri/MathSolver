
using System;
using System.Collections.Generic;

namespace ReversePolishNotation
{
    public class RpnSolver
    {
	    private IRpnTranslator _translator;

	    public RpnSolver(IRpnTranslator translator)
	    {
		    _translator = translator;
	    }

		public double Solve(string expression)
		{
			var stack = new Stack<double>();

			var rpn = this._translator.Translate(expression);
			foreach (var element in rpn)
			{
				element.Calculate(ref stack);
			}

			if (stack.Count != 1)
				throw new InvalidOperationException("Ошибка в выражении: недостаточно операторов");

			return stack.Pop();
		}
    }

}
