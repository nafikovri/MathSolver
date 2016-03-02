using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ReversePolishNotation.Interpreters
{
	public class NumberInterpreter : IRpnInterpreter
	{
		private const string RegexIsNumber = @"^[0-9]+\.?[0-9]*$";

		public bool Interpret(string element, Stack<Operator> stackOfOperators, IList<IRpnElement> rpnOutput)
		{
			if (IsNumber(element))
			{
				rpnOutput.Add(new RpnNumber(Double.Parse(element, new NumberFormatInfo() { CurrencyDecimalSeparator = "." })));
				return true;
			}

			return false;
		}

		private static bool IsNumber(string str)
		{
			return Regex.IsMatch(str, RegexIsNumber);
		}
	}
}