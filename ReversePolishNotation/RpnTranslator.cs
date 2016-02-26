using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReversePolishNotation
{
	public class RpnTranslator : IRpnTranslator
	{
		public static Func<double, double, double> Multiplication = (x, y) => x * y;
		public static Func<double, double, double> Addition = (x, y) => x + y;
		public static Func<double, double, double> Subtraction = (x, y) => x - y;
		public static Func<double, double, double> Division = (x, y) => x / y;

		private readonly ISplitter _splitter;

		private const string _regexIsNumber = @"^[0-9]+\.?[0-9]*$";

		private readonly Dictionary<string, Operator> _operators = new Dictionary<string, Operator>()
		{
			{">>", new Operator() {Priority = 5, RpnOperator = new RpnBinaryOperator((x, y) => (int)x >> (int)y)}},
			{"<<", new Operator() {Priority = 5, RpnOperator = new RpnBinaryOperator((x, y) => (int)x << (int)y)}},

			{"*", new Operator() {Priority = 2, RpnOperator = new RpnBinaryOperator(Multiplication)}},
			{"/", new Operator() {Priority = 2, RpnOperator = new RpnBinaryOperator(Division)}},
			{"+", new Operator() {Priority = 1, RpnOperator = new RpnBinaryOperator(Addition)}},
			{"-", new Operator() {Priority = 1, RpnOperator = new RpnBinaryOperator(Subtraction)}}
		};

		#region Constructor

		public RpnTranslator(ISplitter splitter)
		{
			_splitter = splitter;
		}

		#endregion

		public IEnumerable<IRpnElement> Translate(string expression)
		{
			var result = new List<IRpnElement>();
			var stackOfOperators = new Stack<Operator>();

			foreach (var element in _splitter.Split(expression))
			{
				if (IsNumber(element))
				{
					result.Add(new RpnNumber(double.Parse(element, new NumberFormatInfo() { CurrencyDecimalSeparator = "." })));
				}
				else if (this._operators.ContainsKey(element))
				{
					var oper = this._operators[element];

					while (stackOfOperators.Any() &&
						oper.Priority <= stackOfOperators.Peek().Priority)
					{
						result.Add(stackOfOperators.Pop().RpnOperator);
					}

					stackOfOperators.Push(oper);
				}
				else if ("()".Contains(element))
				{
					if (element.Equals("("))
						stackOfOperators.Push(new Operator() {Priority = 0});
					else
					{
						try
						{
							while (stackOfOperators.Peek().Priority != 0)
							{
								result.Add(stackOfOperators.Pop().RpnOperator);
							}
							stackOfOperators.Pop();
						}
						catch (InvalidOperationException e)
						{
							throw new Exception("Отсутствует открывающая скобка");
						}
					}
				}
				else
				{
					throw new Exception($"Неизвестный элемент выражения: {element}");
				}
			}

			while (stackOfOperators.Any())
			{
				if (stackOfOperators.Peek().RpnOperator == null)
					throw new Exception("Отсутствует закрывающая скобка(и).");

				result.Add(stackOfOperators.Pop().RpnOperator);
			}

			return result;
		}

		private static bool IsNumber(string str)
		{
			return Regex.IsMatch(str, _regexIsNumber);
		}

		private struct Operator
		{
			public int Priority;
			public IRpnElement RpnOperator;
		}
	}
}