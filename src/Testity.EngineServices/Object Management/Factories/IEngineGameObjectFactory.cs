using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using Testity.EngineMath;

namespace Testity.EngineServices
{
	/// <summary>
	/// Implementer provides <see cref="TGameObjectType"/> creation functionality.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Instantiate.html
	/// </summary>
	public interface IEngineGameObjectFactory<TGameObjectType>
		where TGameObjectType : class, IEngineGameObject
    {
		/// <summary>
		/// Creates a <see cref="TGameObjectType"/> at the specified <see cref="Vector3{TMathType}"/> postion and <see cref="Quaternion{TMathType}"/> rotation.
		/// </summary>
		/// <param name="position"><see cref="Vector3{TMathType}"/> position of the <see cref="TGameObjectType"/> to be created.</param>
		/// <param name="rotation"><see cref="Quaternion{TMathType}"/> representing the rotation of the <see cref="TGameObjectType"/> to be created.</param>
		/// <returns>A valid non-null <see cref="TGameObjectType"/> at specified position and rotation.</returns>
		TGameObjectType Create(Vector3<float> position, Quaternion<float> rotation);
	}
}
