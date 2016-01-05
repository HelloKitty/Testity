using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.Common
{
	[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public class EngineComponentMapsToTypeAttribute : ImplementationMetadataAttribute
	{
		public readonly Type ConcreteEngineType;

		public EngineComponentMapsToTypeAttribute(EngineType engineType, string typeName)
			: base(engineType)
		{
			ConcreteEngineType = Type.GetType(typeName);
		}
	}
}
