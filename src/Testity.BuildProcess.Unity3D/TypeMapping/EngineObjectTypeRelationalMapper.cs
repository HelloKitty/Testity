using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class UnityBuildProcessTypeRelationalMapper : ITypeRelationalMapper
	{
		private readonly IEnumerable<ITypeRelationalMapper> typeRelationalMapperChain;

		public UnityBuildProcessTypeRelationalMapper(IEnumerable<ITypeRelationalMapper> mapperChain)
		{
			typeRelationalMapperChain = mapperChain;
        }

		public Type ResolveMappedType(Type typeToFindRelation)
		{
			foreach(ITypeRelationalMapper m in typeRelationalMapperChain)
			{
				Type resultType = m.ResolveMappedType(typeToFindRelation);

				if (resultType != null)
					return resultType;
			}

			throw new ArgumentException("Unable to find mapped type for: " + typeToFindRelation.ToString(), nameof(typeToFindRelation));
		}
	}
}
