using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineComponents
{
	/// <summary>
	/// Meta-data that indicates the marked class is an adapter and indicates that the type
	/// it adapts is the initialized type.
	/// </summary>
	public class EngineComponentAdapterAttribute : Attribute
	{
		/// <summary>
		/// Type this adapter adapts
		/// </summary>
		private readonly Type ActualEngineType;

		public EngineComponentAdapterAttribute(Type requiresEngineType)
		{
			ActualEngineType = requiresEngineType;
        }
	}
}
