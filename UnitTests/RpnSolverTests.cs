using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using ReversePolishNotation;

namespace UnitTests
{
	[TestFixture]
	public class RpnSolverTests
	{
		[Test]
		public void Solve_Correct()
		{
			var mockTranslator = new Mock<IRpnTranslator>();

			#region Set up Mock

			mockTranslator
				.Setup(translator => translator.Translate("4+2*7"))
				.Returns(new List<IRpnElement>()
				{
					new RpnNumber(4),
					new RpnNumber(2),
					new RpnNumber(7),
					new RpnBinaryOperator((x, y) => x*y),
					new RpnBinaryOperator((x, y) => x+y)
				});

			mockTranslator
				.Setup(translator => translator.Translate("3*(6-2)"))
				.Returns(new List<IRpnElement>()
				{
					new RpnNumber(3),
					new RpnNumber(6),
					new RpnNumber(2),
					new RpnBinaryOperator((x, y) => x-y),
					new RpnBinaryOperator((x, y) => x*y)
				});

			mockTranslator
				.Setup(translator => translator.Translate("3-1/2"))
				.Returns(new List<IRpnElement>()
				{
					new RpnNumber(3),
					new RpnNumber(1),
					new RpnNumber(2),
					new RpnBinaryOperator((x, y) => x/y),
					new RpnBinaryOperator((x, y) => x-y)
				});

			#endregion

			var solver = new RpnSolver(mockTranslator.Object);

			Assert.AreEqual(solver.Solve("4+2*7"), 18, 0.002d);
			Assert.AreEqual(solver.Solve("3*(6-2)"), 12, 0.002d);
			Assert.AreEqual(solver.Solve("3-1/2"), 2.5d, 0.002d);
		}
	}
}
