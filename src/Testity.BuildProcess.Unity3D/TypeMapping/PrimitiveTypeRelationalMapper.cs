using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	/// <summary>
	/// Attempts to map a Type to a primitive Type.
	/// </summary>
	public class PrimitiveTypeRelationalMapper : ITypeRelationalMapper
	{
		public Type ResolveMappedType(Type typeToFindRelation)
		{
			//if it's a primitive we just return the type
			if (typeToFindRelation.IsPrimitive)
				return typeToFindRelation;
			else
				return null;
		}
	}
}
