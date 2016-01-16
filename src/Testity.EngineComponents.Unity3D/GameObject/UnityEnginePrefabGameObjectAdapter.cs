using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineComponents.Unity3D
{
	[EngineComponentConcrete(typeof(IEnginePrefabedGameObject))]
	[EngineComponentAdapter(typeof(UnityEngine.GameObject))]
	public class UnityEnginePrefabGameObjectAdapter : UnityEngineGameObjectAdapter, IEnginePrefabedGameObject
	{
		public string PrefabName { get; private set; }

		public UnityEnginePrefabGameObjectAdapter(UnityEngine.GameObject gameObject)
			: base(gameObject)
		{
			//Cache the original name. Consumers can change the name but PrefabName shouldn't change.
			PrefabName = gameObject.name;
		}
	}
}
