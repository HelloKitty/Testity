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
	public interface IEngineGameObjectFactoryService
	{
		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and <see cref="Quaternion{TMathType}"/> rotation.
		/// </summary>
		/// <param name="position"><see cref="Vector3{TMathType}"/> position of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="rotation"><see cref="Quaternion{TMathType}"/> representing the rotation of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="prefabService"><see cref="IEnginePrefabService"/> that provides access to the underlying engine Prefab.</param>
		/// <returns>A valid non-null <see cref="IEnginePrefabedGameObject"/> at specified position and rotation if <see cref="IEnginePrefabService"/> is available.</returns>
		IEnginePrefabedGameObject Create(IEnginePrefabService prefabService, Vector3<float> position, Quaternion<float> rotation);

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and <see cref="Quaternion{TMathType}"/> rotation.
		/// </summary>
		/// <param name="position"><see cref="Vector3{TMathType}"/> position of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="rotation"><see cref="Quaternion{TMathType}"/> representing the rotation of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <returns>A valid non-null <see cref="IEngineGameObject"/> at specified position and rotation.</returns>
		IEngineGameObject Create(Vector3<float> position, Quaternion<float> rotation);
	}
}
