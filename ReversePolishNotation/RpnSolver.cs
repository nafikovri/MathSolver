
namespace ReversePolishNotation
{
    public class RpnSolver
    {
	    private IRpnTranslator _translator;

	    public RpnSolver(IRpnTranslator translator)
	    {
		    _translator = translator;
	    }

		public double Solve(string expression)
		{
			return 42;
		}
    }

}
