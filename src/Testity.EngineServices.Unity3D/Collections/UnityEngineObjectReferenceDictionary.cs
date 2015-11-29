using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices.Unity3D
{
	public class UnityEngineObjectReferenceDictionary<TUnityGameObjectType> : EngineObjectReferenceDictionary<IEngineObject, TUnityGameObjectType>
		where TUnityGameObjectType : UnityEngine.Object
	{
		public UnityEngineObjectReferenceDictionary(IEqualityComparer<IEngineObject> unityEngineEqualityChecker)
			: base(unityEngineEqualityChecker)
		{

		}

		public UnityEngineObjectReferenceDictionary(int initialSize, IEqualityComparer<IEngineObject> unityEngineEqualityChecker)
			: base(initialSize, unityEngineEqualityChecker)
		{

		}
	}
}
