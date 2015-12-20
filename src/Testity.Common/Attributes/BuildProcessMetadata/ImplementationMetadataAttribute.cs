using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.Common
{
	public abstract class ImplementationMetadataAttribute : Attribute
	{
		private readonly EngineType EngineType;

		public ImplementationMetadataAttribute(EngineType type)
		{
			EngineType = type;
        }
	}
}
