using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.Common
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	public class ImplementationRequiresTypeAttribute : Attribute
	{
		public readonly Type ImplementationType;

		public ImplementationRequiresTypeAttribute(Type typeObj)
		{
			ImplementationType = typeObj;
        }
	}
}
