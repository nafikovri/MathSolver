using System.Collections.Generic;

namespace ReversePolishNotation
{
	public class RpnNumber : IRpnElement
	{
		private readonly double _value;

		public RpnNumber(double value)
		{
			this._value = value;
		}

		public void Calculate(ref Stack<double> stack)
		{
			stack.Push(this._value);
		}

		public override bool Equals(object obj)
		{
			var other = obj as RpnNumber;
			if (other != null)
				return this._value.Equals(other._value);

			return false;
		}

		public override int GetHashCode()
		{
			return this._value.GetHashCode();
		}
	}
}