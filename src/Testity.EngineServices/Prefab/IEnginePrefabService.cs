using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineServices
{
	/// <summary>
	/// Provides engine specifc Prefab services.
	/// </summary>
	[EngineServiceInterface]
	public interface IEnginePrefabService
	{
		/// <summary>
		/// The name of the Prefab.
		/// </summary>
		string PrefabName { get; }

		/// <summary>
		/// Indicates if the Prefab service is available. It may not be if uninitialized by the user.
		/// </summary>
		bool isAvailable { get; }
	}
}
