using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Testity.EngineServices.Unity3D
{
	[Serializable]
	public abstract class TestityBehaviour : MonoBehaviour
	{

	}

	[Serializable]
	public class TestityBehaviour<TScriptComponentType> : TestityBehaviour, ITestityBehaviour<TScriptComponentType>, ISerializationCallbackReceiver
		where TScriptComponentType : EngineScriptComponent
	{
		//IDEA: You can create dictionaries for all the service types to serialize them and then draw them later. You can get their underyling draw type via type
		//introspection, maybe load some GUI code that way.

		[SerializeField]
		protected TScriptComponentType _InternalSerializableComponent;

		//[SerializeField]
		//protected UnityEvent testEvent;

		public TScriptComponentType ScriptComponent
		{
			get
			{
				return _InternalSerializableComponent as TScriptComponentType;
			}
		}

		public void OnAfterDeserialize()
		{
			var toInit = _InternalSerializableComponent.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField).Where(x => x.FieldType == typeof(TestUnityEventInterface));

			foreach (var fi in toInit)
			{
				//initialize it if it's null.
				if (fi.GetValue(_InternalSerializableComponent) == null)
				{
					Debug.Log("Setting a new value.");
					fi.SetValue(_InternalSerializableComponent, new UnityEventContainer());
				}

				//((UnityEventContainer)fi.GetValue(_InternalSerializableComponent)).ContainedEvent = testEvent;
			}				
		}

		public void OnBeforeSerialize()
		{
			var toInit = _InternalSerializableComponent.GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField).Where(x => x.FieldType == typeof(TestUnityEventInterface));

			foreach (var fi in toInit)
			{
				//initialize it if it's null.
				if (fi.GetValue(_InternalSerializableComponent) == null)
				{
					Debug.Log("Setting a new value.");
					fi.SetValue(_InternalSerializableComponent, new UnityEventContainer());
				}

				//testEvent = ((UnityEventContainer)fi.GetValue(_InternalSerializableComponent)).ContainedEvent;
			}
		}
	}
}
