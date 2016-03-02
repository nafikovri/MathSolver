using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using ReversePolishNotation;
using ReversePolishNotation.Interpreters;

namespace UnitTests
{
	[TestFixture]
	public class RpnTranslatorTests
	{
		string expCorrect1 = "4+2*7";
		string expCorrect2 = "3*(6-2)";
		string expCorrect3 = "3-1/2";

		string expCorrectDecimal = "3.0-1.5/2.8";

		string expIncorrectParentheses1 = "2+2)";
		string expIncorrectParentheses2 = "(2+2";

		string expIncorrectOperator = "2++2";


		#region Set up Splitters

		private ISplitter SetupSplitter_Correct()
		{
			var mockSplitter = new Mock<ISplitter>();

			#region Set up Mock

			mockSplitter.Setup(splitter => splitter.Split(expCorrect1))
			            .Returns(new List<string>() {"4", "+", "2", "*", "7"});

			mockSplitter.Setup(splitter => splitter.Split(expCorrect2))
			            .Returns(new List<string>() {"3", "*", "(", "6", "-", "2", ")"});

			mockSplitter.Setup(splitter => splitter.Split(expCorrect3))
			            .Returns(new List<string>() {"3", "-", "1", "/", "2"});

			#endregion

			return mockSplitter.Object;
		}

		private ISplitter SetupSplitter_CorrectDecimal()
		{
			var mockSplitter = new Mock<ISplitter>();

			mockSplitter.Setup(splitter => splitter.Split(expCorrectDecimal))
			            .Returns(new List<string>() {"3.0", "-", "1.5", "/", "2.8"});

			return mockSplitter.Object;
		}

		private ISplitter SetupSplitter_IncorrectParentheses()
		{
			var mockSplitter = new Mock<ISplitter>();

			#region Set up Mock

			mockSplitter.Setup(splitter => splitter.Split(expIncorrectParentheses1))
			            .Returns(new List<string>() {"2", "+", "2", ")"});

			mockSplitter.Setup(splitter => splitter.Split(expIncorrectParentheses2))
			            .Returns(new List<string>() {"(", "2", "+", "2"});

			#endregion

			return mockSplitter.Object;
		}

		private ISplitter SetupSplitter_IncorrectOperator()
		{
			var mockSplitter = new Mock<ISplitter>();

			mockSplitter.Setup(splitter => splitter.Split(expIncorrectOperator))
			            .Returns(new List<string>() {"2", "++", "2"});

			return mockSplitter.Object;
		}

		#endregion

		private IList<IRpnInterpreter> GetInterpreters()
		{
			return new List<IRpnInterpreter>
			{
				new BasicOperatorsInterpreter(),
				new ParenthesesInterpreter(),
				new NumberInterpreter()
			};
		}

		[Test]
		public void Translate_Correct()
		{
			var translator = new RpnTranslator(SetupSplitter_Correct(), GetInterpreters());

			CollectionAssert.AreEqual(
				translator.Translate(expCorrect1),
				new List<IRpnElement>()
				{
					new RpnNumber(4),
					new RpnNumber(2),
					new RpnNumber(7),
					new RpnBinaryOperator(BasicOperators.Multiplication),
					new RpnBinaryOperator(BasicOperators.Addition)
				});

			CollectionAssert.AreEqual(
				translator.Translate(expCorrect2),
				new List<IRpnElement>()
				{
					new RpnNumber(3),
					new RpnNumber(6),
					new RpnNumber(2),
					new RpnBinaryOperator(BasicOperators.Subtraction),
					new RpnBinaryOperator(BasicOperators.Multiplication)
				});

			CollectionAssert.AreEqual(
				translator.Translate(expCorrect3),
				new List<IRpnElement>()
				{
					new RpnNumber(3),
					new RpnNumber(1),
					new RpnNumber(2),
					new RpnBinaryOperator(BasicOperators.Division),
					new RpnBinaryOperator(BasicOperators.Subtraction)
				});
		}

		[Test]
		public void Translate_CorrectDecimal()
		{
			var translator = new RpnTranslator(SetupSplitter_CorrectDecimal(), GetInterpreters());

			CollectionAssert.AreEqual(
				translator.Translate(expCorrectDecimal),
				new List<IRpnElement>()
				{
					new RpnNumber(3),
					new RpnNumber(1.5),
					new RpnNumber(2.8),
					new RpnBinaryOperator(BasicOperators.Division),
					new RpnBinaryOperator(BasicOperators.Subtraction)
				});
		}

		[Test]
		public void Translate_IncorrectParentheses()
		{	
			var translator = new RpnTranslator(SetupSplitter_IncorrectParentheses(), GetInterpreters());

			Assert.Throws<Exception>(() => translator.Translate(expIncorrectParentheses1));           // нет открывающей скобки
			Assert.Throws<Exception>(() => translator.Translate(expIncorrectParentheses2));           // нет закрывающей скобки
		}

		[Test]
		public void Translate_IncorrectOperator()
		{
			var translator = new RpnTranslator(SetupSplitter_IncorrectOperator(), GetInterpreters());
			
			Assert.Throws<Exception>(() => translator.Translate(expIncorrectOperator));           // неизвестный оператор
		}
	}
}
