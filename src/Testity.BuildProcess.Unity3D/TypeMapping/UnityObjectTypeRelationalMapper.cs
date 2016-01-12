using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class UnityObjectTypeRelationalMapper : ITypeRelationalMapper
	{
		public Type ResolveMappedType(Type typeToFindRelation)
		{
			//If this is not a UnityEngine.Object then return null
			//Otherwise we have found a Type that is a Unity object and need more information for mapping.
			if (!typeToFindRelation.IsSubclassOf(typeof(UnityEngine.Object)))
				return null;
			else
				return null;
		}
	}
}
