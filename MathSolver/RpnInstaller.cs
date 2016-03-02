using System.Collections.Generic;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using ReversePolishNotation;
using ReversePolishNotation.Interpreters;

namespace MathSolver
{
	class RpnInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For(typeof (ISplitter))
				         .ImplementedBy(typeof (SimpleSplitter))
				         .LifestyleSingleton(),

				Component.For(typeof (IList<IRpnInterpreter>))
				         .UsingFactoryMethod(
					         () => new List<IRpnInterpreter>
					         {
						         new BasicOperatorsInterpreter(),
								 new AdditionalOperatorsInterpreter(),
								 new ParenthesesInterpreter(),
								 new NumberInterpreter()
					         })
				         .LifestyleSingleton(),

				Component.For(typeof (IRpnTranslator))
				         .ImplementedBy(typeof (RpnTranslator))
				         .LifestyleSingleton(),

				Component.For(typeof (RpnSolver))
				         .LifestyleSingleton()
				);
		}
	}
}
