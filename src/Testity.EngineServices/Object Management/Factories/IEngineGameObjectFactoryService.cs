using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using Testity.EngineMath;

namespace Testity.EngineServices
{
	/// <summary>
	/// Implementer provides <see cref="IEngineGameObject"/> and <see cref="IEnginePrefabedGameObject"/> creation functionality.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html
	/// </summary>
	[EngineServiceInterface]
	public interface IEngineGameObjectFactoryService : IEngineGameObjectFactory<IEngineGameObject>
	{
		//MS says don't do this but consumers of this API will find the injection of this type more preferable.
	}
}
