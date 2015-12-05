using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testity.EngineComponents;
using Testity.Serialization;

namespace Testity.Common.Unity3D.Tests
{
	[TestFixture]
	public class EngineScriptComponentFactoryTests
	{
		public class TestEngineScriptBase : EngineScriptComponent
		{
			[ExposeDataMemeber]
			private float privateFloatField;

			[ExposeDataMemeber]
			protected readonly int protectedReadonlyIntField;
		}

		public class TestEngineScriptChild : TestEngineScriptBase
		{
			[ExposeDataMemeber]
			private string privateStringProperty { get; set; }
		}

		[Test]
		public static void Test_EngineScriptComponentFactoryCreate_EngineScriptComponent()
		{
			//arrange
			EngineScriptComponent script = EngineScriptComponentFactory.Create<TestEngineScriptChild>();
			EngineScriptComponent scriptBase = EngineScriptComponentFactory.Create<TestEngineScriptBase>();

			//assert
			//first
			Assert.NotNull(script);
			Assert.IsInstanceOf<TestEngineScriptChild>(script);
			Assert.IsInstanceOf<TestEngineScriptBase>(script);

			//We have to make sure it gives us a proper one even if we've created and cached a similar one before
			//second
			Assert.NotNull(scriptBase);
			Assert.IsInstanceOf<TestEngineScriptBase>(scriptBase);
			Assert.IsNotInstanceOf<TestEngineScriptChild>(scriptBase);
		}

		[Test]
		[MaxTime(int.MaxValue)]
		public static void Test_EngineScriptComponentFactoryCreate_No_Deadlock()
		{
			//This may look add but it's just to try to catch potential deadlocks in the class
			//WARNING: Use a long running task or the test gets funky and won't complete
			IEnumerable<Task> taskCollection = Enumerable.Range(0, 5000).Select((int i) =>
				Task.Factory.StartNew(() =>
				{
					EngineScriptComponentFactory.Create<TestEngineScriptBase>();
					EngineScriptComponentFactory.Create<TestEngineScriptBase>();
				}, TaskCreationOptions.LongRunning));

			//tpl doesn't have great waiting extensions.
			//WaitAll didn't work
			foreach (Task t in taskCollection)
				t.Wait(int.MaxValue);

			//check that all the tasks finished.
			Assert.IsTrue(taskCollection.Select(x => x.IsCompleted).Aggregate(true, (x, y) => x && y));
        }
    }
}
