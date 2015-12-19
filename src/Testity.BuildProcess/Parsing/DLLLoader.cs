using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess
{
	public class DLLLoader
	{
		private readonly string assemblyLocation;

		public DLLLoader(string location)
		{
			assemblyLocation = location;
		}

		public Assembly Load()
		{
			return Assembly.LoadFrom(assemblyLocation);
		}
	}
}
