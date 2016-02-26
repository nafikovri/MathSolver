using System.Collections.Generic;

using NUnit.Framework;

using ReversePolishNotation;

namespace UnitTests
{
	[TestFixture]
	public class SimpleSplitterTests
	{
		[Test]
		public void Split_Correct()
		{
			var splitter = new SimpleSplitter();

			CollectionAssert.AreEqual(
				new List<string>() {"1", "+", "2"},
				splitter.Split("1+2"));

			// выражение некорректное, но это неважно (только разделяем)
			CollectionAssert.AreEqual(
				new List<string>() {"(", "3", "*", "(", "2", "-", "8", ")" },
				splitter.Split("(3*(2-8)"));
		}
	}
}
