using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Testity.Common;
using Testity.Common.Unity3D;
using Testity.EngineComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Testity.EngineComponents.Unity3D
{
	[Serializable]
	public abstract class TestityBehaviour<TScriptComponentType> : MonoBehaviour, ITestityBehaviour<TScriptComponentType>
		where TScriptComponentType : EngineScriptComponent, new()
	{
		//This should be initialized in some way before the game starts.
		[ImplementationField(EngineType.Unity3D, nameof(_InternalSerializableComponent))]
		protected TScriptComponentType _InternalSerializableComponent;

		/// <summary>
		/// The <typeparamref name="TSciptComponentType"/> instance this <see cref="UnityEngine.MonoBehaviour"/> wraps.
		/// </summary>
		public TScriptComponentType ScriptComponent { get { return _InternalSerializableComponent; } }

		/// <summary>
		/// Indicates if the <see cref="TestityBehaviour{TScriptComponentType}"/> has been initialized.
		/// </summary>
		private bool isInitialized = false;

		protected virtual void Start()
		{
			//If we're here and we're not initialized then there is a major problem.
			if (!isInitialized)
				throw new InvalidOperationException(GetType() + " must call " + nameof(Initialize) + " to init internal " + nameof(TScriptComponentType) + " instance.");
		}

		/// <summary>
		/// Initializes the behaviour preparing it for runtime use.
		/// </summary>
		public void Initialize()
		{
			//Creates an empty instance of the TScriptComponentType
			//We do this behind a factory because this may not be a simple task and would emit Activator.CreateInstance
			//if done with new()
			_InternalSerializableComponent = EngineScriptComponentFactory.Create<TScriptComponentType>();

			//this call should set all serialized values
			InitializeScriptComponentMemberValues();

			isInitialized = true;
        }

		/// <summary>
		/// </summary>
		/// <param name="component"></param>
		protected abstract void InitializeScriptComponentMemberValues();
	}
}
