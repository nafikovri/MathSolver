using System.Collections.Generic;

namespace ReversePolishNotation
{
	public class RpnNumber : IRpnElement
	{
		private double _value;

		public RpnNumber(double value)
		{
			this._value = value;
		}

		public void Calculate(ref Stack<double> stack)
		{
			stack.Push(this._value);
		}
	}
}