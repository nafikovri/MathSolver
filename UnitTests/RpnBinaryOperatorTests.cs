
using System;
using System.Collections.Generic;

using NUnit.Framework;

using ReversePolishNotation;

namespace UnitTests
{
	[TestFixture]
	public class RpnBinaryOperatorTests
	{
		[Test]
		public void Calculate_Correct()
		{
			var stack = new Stack<double>(new double[] {1, 2});
			var oper = new RpnBinaryOperator((x, y) => x - y);

			oper.Calculate(ref stack);

			Assert.AreEqual(-1, stack.Peek());
		}

		[Test]
		public void Calculate_Incorrect()
		{
			var stack = new Stack<double>(new double[] {1});
			var oper = new RpnBinaryOperator((x, y) => x - y);

			Assert.Throws<InvalidOperationException>(() => oper.Calculate(ref stack));
		}
	}
}
