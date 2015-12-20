using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.Common
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public class ImplementationRequiresNameAttribute : Attribute
	{
		public readonly string ImplementationName;

		public ImplementationRequiresNameAttribute(string name)
		{
			ImplementationName = name;
        }
	}
}
