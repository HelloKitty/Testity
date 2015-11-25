using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices
{
	/// <summary>
	/// Implementer handlers the lifetime of <see cref="TEngineObjectType"/> objects by managing the factory and destruction classes.
	/// </summary>
	public interface IEngineObjectLifetimeManagementCoordinator<TEngineObjectType> : IEnumerable<TEngineObjectType>, ILifetimeManagedEngineObjectRegister<IEngineObject>, IDisposable
		where TEngineObjectType : class, IEngineObject, IDisposable
    {
		/// <summary>
		/// Indicates the known count of living <see cref="TEngineObjectType"/>s.
		/// </summary>
		int ObjectCount { get; }
	}
}
