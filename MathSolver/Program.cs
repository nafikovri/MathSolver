using System;

using Castle.Windsor;

using ReversePolishNotation;

namespace MathSolver
{
	class Program
	{
		static void Main(string[] args)
		{
			/* В общем случае, контейнер должен быть доступен не только в этом методе,
			 * но для этой небольшой задачи подходит такое решение.
			 */
			var container = new WindsorContainer();
			container.Install(new RpnInstaller());

			var solver = container.Resolve<RpnSolver>();

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
