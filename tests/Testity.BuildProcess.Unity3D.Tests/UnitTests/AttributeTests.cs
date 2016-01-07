using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Testity.Common;
using Testity.EngineComponents;
using Testity.EngineMath;

namespace Testity.BuildProcess.Unity3D.Tests
{
	[TestFixture]
	public static class AttributeTests
	{
		[Test(Author = "Andrew Blakely", Description = "Tests to verify the Type can be generated from the string.", TestOf = typeof(EngineSerializableMapsToTypeAttribute))]
		public static void Check_Can_Resolve_Type_For_Attributes()
		{
			//This finds all components marked with the type mapper attribute and makes sure
			//the types can be resolved and that the type is non-null

			//arrange
			//Assemblies to check
			IEnumerable<Assembly> asses = new List<Assembly> { typeof(IEngineObject).Assembly, typeof(MathT).Assembly };

			//flattens it or whatever
			IEnumerable<Type> allTypes = asses
				.Select(x => x.GetTypes())
				.Aggregate(Enumerable.Empty<Type>(), (s, n) => s.Concat(n));

			//add the ones from the Components dll
			IEnumerable<EngineSerializableMapsToTypeAttribute> typesToCheck = allTypes
				.Where(x => x.GetCustomAttribute<EngineComponentInterfaceAttribute>(false) != null)
				.Where(x => x.GetCustomAttribute<EngineSerializableMapsToTypeAttribute>(false) != null && x.GetCustomAttribute<EngineSerializableMapsToTypeAttribute>(false).EngineType == EngineType.Unity3D)
				.Select(x => x.GetCustomAttribute<EngineSerializableMapsToTypeAttribute>(false));

			//assert
			foreach(EngineSerializableMapsToTypeAttribute t in typesToCheck)
			{
				Assert.NotNull(t);
			}
        }
    }
}
