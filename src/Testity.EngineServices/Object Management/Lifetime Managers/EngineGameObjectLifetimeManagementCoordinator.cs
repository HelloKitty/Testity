using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices
{
	public class EngineGameObjectLifetimeManagementCoordinator : IEngineObjectLifetimeManagementCoordinator<IEngineGameObject>
	{
		public int ObjectCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public IEnumerator<IEngineGameObject> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		public bool Register(IEngineObject objectToRegister)
		{
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
