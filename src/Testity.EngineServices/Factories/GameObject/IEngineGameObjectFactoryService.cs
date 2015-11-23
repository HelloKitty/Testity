using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using Testity.EngineMath;

namespace Testity.EngineServices
{
	/// <summary>
	/// Provides GameObject creation services.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html
	/// </summary>
	public interface IEngineGameObjectFactoryService
	{
		/// <summary>
		/// Creates an empty and default <see cref="IEngineGameObject"/> at the default <see cref="Vector3{TMathType}"/> position and rotation.
		/// </summary>
		/// <returns>A valid non-null <see cref="IEngineGameObject"/> at the default location and rotation.</returns>
		IEngineGameObject CreateEmptyDefault();

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and rotation.
		/// </summary>
		/// <param name="worldPosition">World position of the <see cref="IEngineGameObject"/> to be created.</param>
		/// <param name="eulerRotation"><see cref="Vector3{TMathType}"/> representing the world rotation of the <see cref="IEngineGameObject"/> to be created.</param>
		/// <returns>A valid non-null <see cref="IEngineGameObject"/> at worldPostion with a rotation of eulerRotation.</returns>
		IEngineGameObject Create(Vector3<float> worldPosition, Vector3<float> eulerRotation);

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and <see cref="Quaternion{TMathType}"/> rotation.
		/// </summary>
		/// <param name="worldPosition">World position of the <see cref="IEngineGameObject"/> to be created.</param>
		/// <param name="quatRotation"><see cref="Quaternion{TMathType}"/> representing the world rotation of the <see cref="IEngineGameObject"/> to be created.</param>
		/// <returns>A valid non-null <see cref="IEngineGameObject"/> at worldPostion with quatRotation.</returns>
		IEngineGameObject Create(Vector3<float> worldPosition, Quaternion<float> quatRotation);
	}
}
