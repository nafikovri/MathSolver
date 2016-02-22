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
			var moqTranslator = new Mock<IRpnTranslator>();

			// todo: set up Moq

			var solver = new RpnSolver(moqTranslator.Object);

			Assert.AreEqual(solver.Solve("4+2*7"), 18, 0.002d);
			Assert.AreEqual(solver.Solve("3*(6-2)"), 12, 0.002d);
			Assert.AreEqual(solver.Solve("3-1/2"), 2.5d, 0.002d);
		}
	}
}
