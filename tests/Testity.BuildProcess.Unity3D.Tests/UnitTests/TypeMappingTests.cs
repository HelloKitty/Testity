using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.BuildProcess;

namespace Testity.BuildProcess.Unity3D.Tests
{
	[TestFixture]
	public static class TypeMappingTests
	{
		//a lot of these types for testing are just picked at random. Just whatever pops up in intelisense

		[Test(Author = "Andrew Blakely", Description = "Ensures mapper returns null for invalid types.", TestOf = typeof(DefaultTypeRelationalMapper))]
		[TestCase(typeof(string))]
		[TestCase(typeof(StringBuilder))]
		[TestCase(typeof(EngineScriptComponentLocator))]
		public static void Test_DefaultTypeRelationMapper_Returns_Null_On_Invalid_Types(Type t)
		{
			//arrange
			ITypeRelationalMapper mapper = new DefaultTypeRelationalMapper();

			//assert
			Assert.IsNull(mapper.ResolveMappedType(t));
        }

		[Test(Author = "Andrew Blakely", Description = "Ensures mapper returns valid type for valid inputs.", TestOf = typeof(DefaultTypeRelationalMapper))]
		[TestCase(typeof(IServiceProvider))]
		public static void Test_DefaultTypeRelationMapper_Returns_Expected_Type(Type t)
		{
			//arrange
			ITypeRelationalMapper mapper = new DefaultTypeRelationalMapper();

			//assert
			Assert.NotNull(mapper.ResolveMappedType(t));
		}
	}
}
