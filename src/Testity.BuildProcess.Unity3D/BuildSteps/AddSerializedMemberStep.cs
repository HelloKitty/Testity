using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fasterflect;

namespace Testity.BuildProcess.Unity3D
{
	public class AddSerializedMemberStep : ITestityBuildStep
	{
		private readonly ITypeRelationalMapper typeResolver;

		private readonly ITypeMemberParser typeParser;

		public AddSerializedMemberStep(ITypeRelationalMapper mapper, ITypeMemberParser parser)
		{
			typeResolver = mapper;
			typeParser = parser;
        }

		public void Process(IClassBuilder builder, Type typeToParse)
		{
			//This can be done in a way that preserves order but that is not important in the first Testity build.
			//We can improve on that later

			//handle serialized fields and properties
			foreach (MemberInfo mi in typeParser.Parse(MemberTypes.Field | MemberTypes.Property, typeToParse))
			{
				builder.AddClassField(new UnitySerializedFieldImplementationProvider(mi.Name, typeResolver.ResolveMappedType(mi.Type()), new Common.Unity3D.WiredToAttribute(mi.MemberType, mi.Name, mi.Type())));
            }
		}
	}
}
