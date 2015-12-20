using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.Common.Attributes
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public class ImplementationRequiresParametersAttribute : Attribute
	{
		public readonly IEnumerable<Type> ImplementationParameterTypes;

		public ImplementationRequiresParametersAttribute(params Type[] parameters)
		{
			ImplementationParameterTypes = parameters;
		}
	}
}
