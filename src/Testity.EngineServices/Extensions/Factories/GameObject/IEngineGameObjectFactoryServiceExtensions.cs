using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;
using Testity.EngineMath;

namespace Testity.EngineServices
{
	/// <summary>
	/// Extends the functionality and API of <see cref="IEngineGameObjectFactoryService"/>
	/// </summary>
	public static class IEngineGameObjectFactoryServiceExtensions
	{
		
		//Extends the prefab creation method with additional overloads for the user.
		#region Prefab Creation Extensions

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and euler <see cref="Vector3{TMathType}"/> rotation.
		/// </summary>
		/// <param name="position"><see cref="Vector3{TMathType}"/> position of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="rotation"><see cref="Vector3{TMathType}"/> representing the euler vector rotation of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="prefabService"><see cref="IEnginePrefabService"/> that provides access to the underlying engine Prefab.</param>
		/// <param name="factoryService">Factory instace that is being extended via this extension method.</param>
		/// <returns>A valid non-null <see cref="IEnginePrefabedGameObject"/> at specified position and rotation if <see cref="IEnginePrefabService"/> is available.</returns>
		public static IEnginePrefabedGameObject Create(this IEngineGameObjectFactoryService factoryService, IEnginePrefabService prefabService, Vector3<float> position, Vector3<float> rotation)
		{
			return factoryService.Create(prefabService, position, rotation.Euler());
		}

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and no rotation.
		/// </summary>
		/// <param name="position"><see cref="Vector3{TMathType}"/> position of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="prefabService"><see cref="IEnginePrefabService"/> that provides access to the underlying engine Prefab.</param>
		/// <param name="factoryService">Factory instace that is being extended via this extension method.</param>
		/// <returns>A valid non-null <see cref="IEnginePrefabedGameObject"/> at specified position and no rotation if <see cref="IEnginePrefabService"/> is available.</returns>
		public static IEnginePrefabedGameObject Create(this IEngineGameObjectFactoryService factoryService, IEnginePrefabService prefabService, Vector3<float> position)
		{
			return factoryService.Create(prefabService, position, Quaternion<float>.identity);
		}

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the origin with no rotation.
		/// </summary>
		/// <param name="prefabService"><see cref="IEnginePrefabService"/> that provides access to the underlying engine Prefab.</param>
		/// <param name="factoryService">Factory instace that is being extended via this extension method.</param>
		/// <returns>A valid non-null <see cref="IEnginePrefabedGameObject"/> at specified position and no rotation if <see cref="IEnginePrefabService"/> is available.</returns>
		public static IEnginePrefabedGameObject Create(this IEngineGameObjectFactoryService factoryService, IEnginePrefabService prefabService)
		{
			return factoryService.Create(prefabService, Vector3<float>.zero, Quaternion<float>.identity);
		}

		#endregion

		#region GameObject Creation Extensions

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and euler <see cref="Vector3{TMathType}"/> rotation.
		/// </summary>
		/// <param name="position"><see cref="Vector3{TMathType}"/> position of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="rotation"><see cref="Vector3{TMathType}"/> representing the euler vector rotation of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="factoryService">Factory instace that is being extended via this extension method.</param>
		/// <returns>A valid non-null <see cref="IEngineGameObject"/> at specified position and rotation.</returns>
		public static IEngineGameObject Create(this IEngineGameObjectFactoryService factoryService, Vector3<float> position, Vector3<float> rotation)
		{
			return factoryService.Create(position, rotation.Euler());
		}

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the specified <see cref="Vector3{TMathType}"/> postion and no rotation.
		/// </summary>
		/// <param name="position"><see cref="Vector3{TMathType}"/> position of the <see cref="IEnginePrefabedGameObject"/> to be created.</param>
		/// <param name="factoryService">Factory instace that is being extended via this extension method.</param>
		/// <returns>A valid non-null <see cref="IEngineGameObject"/> at specified position and no rotation.</returns>
		public static IEngineGameObject Create(this IEngineGameObjectFactoryService factoryService, Vector3<float> position)
		{
			return factoryService.Create(position, Quaternion<float>.identity);
		}

		/// <summary>
		/// Creates a default <see cref="IEngineGameObject"/> at the origin with no rotation.
		/// </summary>
		/// <param name="factoryService">Factory instace that is being extended via this extension method.</param>
		/// <returns>A valid non-null <see cref="IEngineGameObject"/> at specified position and default rotation.</returns>
		public static IEngineGameObject Create(this IEngineGameObjectFactoryService factoryService)
		{
			return factoryService.Create(Vector3<float>.zero, Quaternion<float>.identity);
		}

		#endregion
	}
}
