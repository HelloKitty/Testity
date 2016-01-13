using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testity.EngineComponents;
using Testity.EngineComponents.Unity3D;

namespace Testity.BuildProcess.Unity3D
{
	/// <summary>
	/// Default or last-chance type relational mapper that attempts to map a type to a Unity serializable type.
	/// It can map interfaces. Classes would have to be EngineScriptComponents and those are handled elsewhere
	/// </summary>
	public class DefaultTypeRelationalMapper : ITypeRelationalMapper
	{
		public Type ResolveMappedType(Type typeToFindRelation)
		{
			//This allows users to serialize interfaces that EngineScriptComponent types may implement
			//Unity editor will not allow users to set TestityBehaviour's that don't contain EngineScriptComponents that implement the interface

			//Exclude types that aren't interfaces
			if (typeToFindRelation.IsInterface)
				return typeToFindRelation;
			else
				return null;
		}
	}
}
