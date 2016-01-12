using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class AddSerializedMemberStep : ITestityBuildStep
	{
		private readonly ITypeRelationalMapper typeResolver;

		public AddSerializedMemberStep(ITypeRelationalMapper mapper)
		{
			typeResolver = mapper;
        }

		public void Process(IClassBuilder builder, Type typeToParse)
		{
			//This can be done in a way that preserves order but that is not important in the first Testity build.
			//We can improve on that later

			//handle serialized properties first
			foreach (PropertyInfo pi in SerializedMemberParser<PropertyInfo>.Parse(typeToParse))
			{
				builder.AddClassField(new UnitySerializedFieldImplementationProvider(pi.Name, typeResolver.ResolveMappedType(pi.PropertyType), new Common.Unity3D.WiredToAttribute(MemberTypes.Property, pi.Name)));
            }

			//handle serialized fields second
			foreach (FieldInfo pi in SerializedMemberParser<FieldInfo>.Parse(typeToParse))
			{
				builder.AddClassField(new UnitySerializedFieldImplementationProvider(pi.Name, typeResolver.ResolveMappedType(pi.FieldType), new Common.Unity3D.WiredToAttribute(MemberTypes.Field, pi.Name)));
			}
		}
	}
}
