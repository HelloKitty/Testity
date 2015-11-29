using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Testity.EngineServices.Unity3D
{
	[Serializable]
	public class SerializationTestBehaviour : TestityBehaviour<TestScriptComponent>
	{
		public void Update()
		{
			this._InternalSerializableComponent.Update();
		}
	}
}
