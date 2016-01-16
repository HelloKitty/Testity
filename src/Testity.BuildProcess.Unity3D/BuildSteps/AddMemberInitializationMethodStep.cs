using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class AddMemberInitializationMethodStep : ITestityBuildStep
	{
		private readonly ITypeRelationalMapper typeResolver;

		private readonly ITypeMemberParser typeParser;

		public AddMemberInitializationMethodStep(ITypeRelationalMapper mapper, ITypeMemberParser parser)
		{
			typeResolver = mapper;
			typeParser = parser;
		}

		public void Process(IClassBuilder builder, Type typeToParse)
		{
			//Gather all fields and properties
			IEnumerable<MemberInfo> infos = typeParser.Parse(MemberTypes.Field | MemberTypes.Property, typeToParse);

			//foreach serialized Type we need to take the actual Type and then Serialize type and use it to find an adapter
			//foreach()

            //mi.Name, typeResolver.ResolveMappedType(mi.Type()), new Common.Unity3D.WiredToAttribute(mi.MemberType, mi.Name, mi.Type())));
		}
	}
}
