using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices
{
	public interface ILifetimeManagedEngineObjectRegister<TEngineObjectType>
		where TEngineObjectType : class, IEngineObject
    {
		bool Register(TEngineObjectType objectToRegister);
	}
}
