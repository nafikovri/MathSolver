using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReversePolishNotation
{
	public class SimpleSplitter : ISplitter
	{
		private const string _regexSplitter = @"([0-9]+\.?[0-9]*|[\(\)])";

		public IEnumerable<string> Split(string expression)
		{
			expression = Regex.Replace(expression, @"\s", string.Empty);

			var elements = Regex.Split(expression, _regexSplitter);
			return elements.Where(element => element.Length != 0);
		}
	}
}