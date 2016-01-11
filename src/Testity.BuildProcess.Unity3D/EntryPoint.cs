using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Testity.BuildProcess.Unity3D
{
	public static class EntryPoint
	{
		public static void Main(string[] args)
		{
			//There may be uncaught exceptions so we'll handle them all gracefully.
			//There probably is no way to recover from an exception during this process so catching is fruitless really
			AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

			Assembly loadedAss = LoadAssembly(args);

			//We assume this is a testity assembly
			//If someone called this builder then they WANT a Testity generated dll either way.

			//load the EngineScriptComponentTypes
			IEnumerable<Type> potentialBehaviourTypes = LoadBehaviourTypes(loadedAss);


		}

		private static IEnumerable<Type> LoadBehaviourTypes(Assembly loadedAss)
		{
			EngineScriptComponentLocator locator = new EngineScriptComponentLocator(loadedAss);

			//Should return types that are EngineScriptComponents
			return locator.LoadTypes();
	}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Console.WriteLine(sender.ToString() + " caused exception: " + e.ExceptionObject.ToString());
			Console.WriteLine("Press any key to continue...");
			Console.ReadKey();
		}

		private static Assembly LoadAssembly(string[] args) 
		{
			//Let's try to load the assembly passed in again.
			//Doesn't have to be reflection only but shouldn't need a full load
			try
			{
				return Assembly.ReflectionOnlyLoadFrom(args[0]);
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine("Testity requires the dll path to parse/compile from as an argument.");
				throw;
			}
		}
	}
}
