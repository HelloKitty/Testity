using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using Testity.EngineMath;

namespace Testity.EngineServices
{
	public class LifetimeManagedGameObjectFactoryAdapter : IEngineGameObjectFactory<IEngineGameObject>
	{
		private readonly IEngineGameObjectFactory<IEngineGameObject> lifetimeManagedFactory;

		private readonly ILifetimeManagedEngineObjectRegister<IEngineGameObject> lifetimeManagerRegister;

		public LifetimeManagedGameObjectFactoryAdapter(IEngineGameObjectFactory<IEngineGameObject> factoryToManage, ILifetimeManagedEngineObjectRegister<IEngineGameObject> lifetimeRegister)
		{
			lifetimeManagedFactory = factoryToManage;
		}

		public IEngineGameObject Create(Vector3<float> position, Quaternion<float> rotation)
		{
			IEngineGameObject gameObject = lifetimeManagedFactory.Create(position, rotation);

			if (gameObject == null)
				throw new InvalidOperationException(nameof(IEngineGameObjectFactory<IEngineGameObject>) + " field " + nameof(lifetimeManagedFactory) + " failed to produce a valid " + nameof(IEngineGameObject));

			if (!lifetimeManagerRegister.Register(gameObject))
				throw new InvalidOperationException("Failed to register " + nameof(gameObject) + " created by " + nameof(lifetimeManagedFactory) + " in registry " + nameof(lifetimeManagerRegister) + " of type " + lifetimeManagerRegister.GetType());

			return gameObject;
        }
	}
}
