using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess
{
	public class ParameterData
	{
		public readonly Type ParameterType;

		public readonly string ParameterName;

		public ParameterData(Type paramType, string paramName)
		{
			ParameterType = paramType;
			ParameterName = paramName;
		}
	}
}
