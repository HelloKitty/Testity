using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices
{
	/// <summary>
	/// Implementer provides information about a Prefab instance and <see cref="IEnginePrefabedGameObject"/> creation functionality.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html
	/// </summary>
	[EngineServiceInterface]
	public interface IEnginePrefabService : IEngineGameObjectFactoryService<IEnginePrefabedGameObject>
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
