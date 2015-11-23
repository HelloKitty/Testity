using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices
{
	/// <summary>
	/// Implementer provides factory/creation services for TComponentType objects.
	/// Also makes the promise of managing dependencies for construction.
	/// </summary>
	/// <typeparam name="TComponentType">Type to be created.</typeparam>
	public interface IDependencyInjectionManagedFactory<TCreationType>
		where TCreationType : class
	{
		TCreationType Create();

		IEnumerable<TCreationType> CreateMany(int count);
	}
}
