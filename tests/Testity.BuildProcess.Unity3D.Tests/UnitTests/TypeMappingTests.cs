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
		[TestCase(typeof(IEnumerable<int>))]
		public static void Test_DefaultTypeRelationMapper_Returns_Expected_Type(Type t)
		{
			//arrange
			ITypeRelationalMapper mapper = new DefaultTypeRelationalMapper();

			//act
			Type mappedType = mapper.ResolveMappedType(t);

			//assert
			//if it's an interface it should be MonoBehaviour
			Assert.AreEqual(typeof(UnityEngine.MonoBehaviour), mappedType);
		}

		[Test(Author = "Andrew Blakely", Description = "Ensures mapper returns valid type for valid inputs.", TestOf = typeof(PrimitiveTypeRelationalMapper))]
		[TestCase(typeof(int))]
		[TestCase(typeof(Int32))]
		[TestCase(typeof(ushort))]
		[TestCase(typeof(byte))]
		public static void Test_PrimitiveTypeMapper_Returns_Expected_Type(Type t)
		{
			//arrange
			ITypeRelationalMapper mapper = new PrimitiveTypeRelationalMapper(Enumerable.Empty<Type>());

			//act
			Type mappedType = mapper.ResolveMappedType(t);

			//assert
			Assert.AreEqual(t, mappedType);
		}

		[Test(Author = "Andrew Blakely", Description = "Ensures mapper returns null for invalid types.", TestOf = typeof(PrimitiveTypeRelationalMapper))]
		[TestCase(typeof(IServiceProvider))]
		[TestCase(typeof(IEnumerable<int>))]
		[TestCase(typeof(string))] //string is not a primitve
		public static void Test_PrimitiveTypeMapper_Returns_Null_On_Invalid_Type(Type t)
		{
			//arrange
			ITypeRelationalMapper mapper = new PrimitiveTypeRelationalMapper(Enumerable.Empty<Type>());

			//act
			Type mappedType = mapper.ResolveMappedType(t);

			//assert
			Assert.IsNull(mappedType);
		}
	}
}
