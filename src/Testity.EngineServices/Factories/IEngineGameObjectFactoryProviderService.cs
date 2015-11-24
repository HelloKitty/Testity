using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices
{
	/// <summary>
	/// Provides <see cref="IEngineGameObjectFactory"/>s for creating <see cref="IEngineGameObject"/>s.
	/// Loosely based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html
	/// </summary>
	[EngineServiceInterface]
	public interface IEngineGameObjectFactoryProviderService
	{
		/// <summary>
		/// Provides a valid <see cref="IEngineGameObjectFactory{IEnginePrefabedGameObject}"/> that can handle creating <see cref="IEnginePrefabService"/>.
		/// </summary>
		/// <param name="prefabService"></param>
		/// <returns>A factory for <see cref="IEngineGameObjectFactory{IEnginePrefabedGameObject}"/>s.</returns>
		IEngineGameObjectFactory<IEnginePrefabedGameObject> Factory(IEnginePrefabService prefabService);

		/// <summary>
		/// Provides a valid <see cref="IEngineGameObjectFactory"/> that can hande creating non-prefabbed (empty) <see cref="IEngineGameObject"/>s.
		/// </summary>
		/// <returns>A factory for <see cref="IEngineGameObject"/>s</returns>
		IEngineGameObjectFactory Factory();
	}
}
