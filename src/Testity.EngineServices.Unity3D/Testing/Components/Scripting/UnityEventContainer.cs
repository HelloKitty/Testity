using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

namespace Testity.EngineServices.Unity3D
{
	[Serializable]
	public class UnityEventContainer : TestUnityEventInterface
	{
		[SerializeField]
		public UnityEvent ContainedEvent;
	}
}
