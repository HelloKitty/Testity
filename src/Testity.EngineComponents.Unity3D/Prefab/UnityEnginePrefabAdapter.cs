using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineComponents
{
	[EngineComponentConcrete(typeof(IEnginePrefab))]
	[Serializable]
	public class UnityEnginePrefabAdapter : IEnginePrefab
	{
		//Should be available as long as it's not null
		public bool isAvailable { get { return prefabbedGameObjectInstance != null; } }

		public string PrefabName { get; private set; }

		private readonly UnityEngine.GameObject prefabbedGameObjectInstance;

		public UnityEnginePrefabAdapter(UnityEngine.GameObject gameObjectPrefab)
		{
			if (gameObjectPrefab == null)
				throw new ArgumentNullException(nameof(gameObjectPrefab), nameof(UnityEngine.GameObject) + " must not be null.");

			//Could change so we should grab it now
			PrefabName = prefabbedGameObjectInstance.name;

			prefabbedGameObjectInstance = gameObjectPrefab;
	}
	}
}
