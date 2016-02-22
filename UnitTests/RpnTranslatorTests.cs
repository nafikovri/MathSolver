using System;
using System.Collections.Generic;

using NUnit.Framework;

using ReversePolishNotation;

namespace UnitTests
{
	[TestFixture]
	public class RpnTranslatorTests
	{
		[Test]
		public void Translate_Correct()
		{
			var translator = new RpnTranslator();

			CollectionAssert.AreEqual(
				translator.Translate("4+2*7"),
				new List<IRpnElement>()
				{
					new RpnNumber(4),
					new RpnNumber(2),
					new RpnNumber(7),
					new RpnBinaryOperator(RpnTranslator.Multiplication),
					new RpnBinaryOperator(RpnTranslator.Addition)
				});

			CollectionAssert.AreEqual(
				translator.Translate("3*(6-2)"),
				new List<IRpnElement>()
				{
					new RpnNumber(3),
					new RpnNumber(6),
					new RpnNumber(2),
					new RpnBinaryOperator(RpnTranslator.Subtraction),
					new RpnBinaryOperator(RpnTranslator.Multiplication)
				});

			CollectionAssert.AreEqual(
				translator.Translate("3-1/2"),
				new List<IRpnElement>()
				{
					new RpnNumber(3),
					new RpnNumber(1),
					new RpnNumber(2),
					new RpnBinaryOperator(RpnTranslator.Division),
					new RpnBinaryOperator(RpnTranslator.Subtraction)
				});
		}

		[Test]
		public void Translate_IncorrectParentheses()
		{
			var translator = new RpnTranslator();

			Assert.Throws<Exception>(() => translator.Translate("2+2)"));           // нет открывающей скобки
			Assert.Throws<Exception>(() => translator.Translate("(2+2"));           // нет закрывающей скобки
		}

		[Test]
		public void Translate_IncorrectOperator()
		{
			var translator = new RpnTranslator();
			
			Assert.Throws<Exception>(() => translator.Translate("2++2"));           // неизвестный оператор
		}
	}
}
