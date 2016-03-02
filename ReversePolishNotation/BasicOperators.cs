using System;

namespace ReversePolishNotation
{
	public static class BasicOperators
	{
		public static Func<double, double, double> Multiplication = (x, y) => x * y;
		public static Func<double, double, double> Addition = (x, y) => x + y;
		public static Func<double, double, double> Subtraction = (x, y) => x - y;
		public static Func<double, double, double> Division = (x, y) => x / y;
	}
}
