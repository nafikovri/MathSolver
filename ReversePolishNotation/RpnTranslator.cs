using System;
using System.Collections.Generic;
using System.Linq;

namespace ReversePolishNotation
{
	public class RpnTranslator : IRpnTranslator
	{
		private readonly ISplitter _splitter;
		private readonly IList<IRpnInterpreter> _interpreters;

		#region Constructor

		public RpnTranslator(ISplitter splitter, IList<IRpnInterpreter> interpreters)
		{
			_splitter = splitter;
			_interpreters = interpreters;
		}

		#endregion

		public IEnumerable<IRpnElement> Translate(string expression)
		{
			var result = new List<IRpnElement>();
			var stackOfOperators = new Stack<Operator>();

			foreach (var element in _splitter.Split(expression))
			{
				var wasProcessed = _interpreters.Any(interpreter => interpreter.Interpret(element, stackOfOperators, result));

				if (!wasProcessed)
					throw new Exception($"Неизвестный элемент выражения: {element}");
			}

			while (stackOfOperators.Any())
			{
				if (stackOfOperators.Peek().RpnOperator == null)
					throw new Exception("Отсутствует закрывающая скобка(и).");

				result.Add(stackOfOperators.Pop().RpnOperator);
			}

			return result;
		}
	}
}