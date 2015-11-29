using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using UnityEngine.Events;

namespace Testity.EngineServices.Unity3D
{
	[Serializable]
	public class TestScriptComponent : EngineScriptComponent, IEngineUpdateable
	{
		public string Test;

		public IEnginePrefabService prefabTest;

		public TestUnityEventInterface testEvent;

		public string NewTest;

		public void Update()
		{
			((UnityEventContainer)testEvent).ContainedEvent.Invoke();
		}
	}
}
