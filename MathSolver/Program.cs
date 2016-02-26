using System;

using ReversePolishNotation;

namespace MathSolver
{
	class Program
	{
		static void Main(string[] args)
		{
			var solver = new RpnSolver(new RpnTranslator(new SimpleSplitter()));

			while (true)
			{
				Console.Write("Введите выражение: ");
				var expression = Console.ReadLine();

				try
				{
					Console.WriteLine("Результат: {0}", solver.Solve(expression));
				}
				catch (Exception e)
				{
					Console.WriteLine("Ошибка: {0}", e.Message);
				}

				Console.WriteLine();
			}
		}
	}
}
