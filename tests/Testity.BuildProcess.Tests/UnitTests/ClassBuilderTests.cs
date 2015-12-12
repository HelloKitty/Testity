using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testity.EngineComponents;
using Testity.Serialization;

namespace Testity.BuildProcess.Tests
{
	[TestFixture]
	public static class ClassBuilderTests
	{
		[Test(Author = "Andrew Blakely", Description = "Tests the Rosyln compilation addition of using statements TestityClassBuilder.", TestOf = typeof(TestityClassBuilder<>))]
		public static void Test_TestityClassBuilder_TestAddedField()
		{
			//arrange
			TestityClassBuilder<EngineScriptComponent> scriptBuilder = new TestityClassBuilder<EngineScriptComponent>();

			//act
			scriptBuilder.AddClassField("testField", typeof(EngineScriptComponent), MemberImplementationModifier.Private, new ExposeDataMemeberAttribute());
			scriptBuilder.AddClassField("testField5", typeof(EngineScriptComponent), MemberImplementationModifier.Private, new ExposeDataMemeberAttribute(), new ThreadStaticAttribute());

			//assert
			Assert.IsTrue(scriptBuilder.Compile().Contains("private " + typeof(EngineScriptComponent).FullName + " testField"));
			Assert.IsTrue(scriptBuilder.Compile().Contains("[" + typeof(ExposeDataMemeberAttribute).FullName+ "]"));
        }

		[Test(Author = "Andrew Blakely", Description = "Tests the Rosyln compilation adding of a base class with TestityClassBuilder.", TestOf = typeof(TestityClassBuilder<>))]
		public static void Test_TestityClassBuilder_Test_Adding_Base_Class()
		{
			//arrange
			TestityClassBuilder<EngineScriptComponent> scriptBuilder = new TestityClassBuilder<EngineScriptComponent>();

			//act
			scriptBuilder.AddBaseClass<EngineScriptComponent>();

			//assert
			Assert.IsTrue(scriptBuilder.Compile().Contains(" : " + typeof(EngineScriptComponent).FullName));
			Assert.Throws<InvalidOperationException>(() => scriptBuilder.AddBaseClass<EngineScriptComponent>());
			Assert.DoesNotThrow(() => scriptBuilder.AddBaseClass<ICloneable>());
			Assert.IsTrue(scriptBuilder.Compile().Contains(", " + typeof(ICloneable).FullName));
		}

		[Test(Author = "Andrew Blakely", Description = "Tests the Rosyln compilation adding of a method with TestityClassBuilder.", TestOf = typeof(TestityClassBuilder<>))]
		public static void Test_TestityClassBuilder_Test_Adding_Method()
		{
			//arrange
			TestityClassBuilder<EngineScriptComponent> scriptBuilder = new TestityClassBuilder<EngineScriptComponent>();

			//act
			scriptBuilder.AddMemberMethod("TestMethod", typeof(string), MemberImplementationModifier.Public, null, new ParameterData(typeof(string), "paramOne"), new ParameterData(typeof(int), "paramTwo"));

			//assert
			Assert.IsTrue(scriptBuilder.Compile().Contains("TestMethod(System.String paramOne"));
		}
	}
}
