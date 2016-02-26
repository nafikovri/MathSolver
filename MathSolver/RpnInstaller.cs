using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using ReversePolishNotation;

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

				Component.For(typeof (IRpnTranslator))
				         .ImplementedBy(typeof (RpnTranslator))
				         .LifestyleSingleton(),

				Component.For(typeof (RpnSolver))
				         .LifestyleSingleton()
				);
		}
	}
}
