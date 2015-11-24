using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using Testity.EngineMath;

namespace Testity.EngineServices
{
	/// <summary>
	/// Provides GameObject creation services but is not a service.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html
	/// </summary>
	public interface IEngineGameObjectFactory : IEngineGameObjectFactory<IEngineGameObject>
	{

    }

	public interface IEngineGameObjectFactory<TGameObjectType>
		where TGameObjectType : class, IEngineGameObject
    {
		/// <summary>
		/// Creates a <see cref="TGameObjectType"/> at the default <see cref="Vector3{TMathType}"/> position and rotation.
		/// </summary>
		/// <returns>A valid non-null <see cref="TGameObjectType"/> at the default location and rotation.</returns>
		TGameObjectType WithDefaultOrientation();

		/// <summary>
		/// Creates a default <see cref="TGameObjectType"/> at the specified <see cref="Vector3{TMathType}"/> postion with default rotation.
		/// </summary>
		/// <param name="worldPosition">World position of the <see cref="TGameObjectType"/> to be created.</param>
		/// <returns>A valid non-null <see cref="TGameObjectType"/> at worldPostion with default rotation.</returns>
		TGameObjectType With(Vector3<float> worldPosition);

		/// <summary>
		/// Creates a default <see cref="TGameObjectType"/> at the specified <see cref="Vector3{TMathType}"/> postion and rotation.
		/// </summary>
		/// <param name="worldPosition">World position of the <see cref="TGameObjectType"/> to be created.</param>
		/// <param name="eulerRotation"><see cref="Vector3{TMathType}"/> representing the world rotation of the <see cref="TGameObjectType"/> to be created.</param>
		/// <returns>A valid non-null <see cref="TGameObjectType"/> at worldPostion with a rotation of eulerRotation.</returns>
		TGameObjectType With(Vector3<float> worldPosition, Vector3<float> eulerRotation);

		/// <summary>
		/// Creates a default <see cref="TGameObjectType"/> at the specified <see cref="Vector3{TMathType}"/> postion and <see cref="Quaternion{TMathType}"/> rotation.
		/// </summary>
		/// <param name="worldPosition">World position of the <see cref="TGameObjectType"/> to be created.</param>
		/// <param name="quatRotation"><see cref="Quaternion{TMathType}"/> representing the world rotation of the <see cref="TGameObjectType"/> to be created.</param>
		/// <returns>A valid non-null <see cref="TGameObjectType"/> at worldPostion with quatRotation.</returns>
		TGameObjectType With(Vector3<float> worldPosition, Quaternion<float> quatRotation);
	}
}
