using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineComponents
{
	//TODO: Update interface and doc with UnrealEngine4's equivalents
	//WARNING: To future maintainers. We want people to be able to do == to check for equivalence but if they're different
	//instances of the same interface then it'll return false. We can't overload in implementer, it will not call implementer's overloaded operator
	//so these need to be 1:1 for GameObjects.
	/// <summary>
	/// Provides access to all base GameObject functionality.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/GameObject.html
	/// Based on methods in UnrealEngine 4's: https://docs.unrealengine.com/latest/INT/API/Runtime/Engine/GameFramework/AActor/index.html
	/// </summary>
	[EngineComponentInterface]
	public interface IEngineGameObject : IEngineObject, IEngineActivatable, IEquatable<IEngineGameObject>
	{
		/// <summary>
		/// Transform component that represents the objects transformation.
		/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/GameObject-transform.html
		/// Based on UnrealEngine 4's: https://docs.unrealengine.com/latest/INT/API/Runtime/Engine/GameFramework/AActor/GetTransform/index.html
		/// </summary>
		IEngineTransform Transform { get; }

		/// <summary>
		/// Directional component that provides directions based on the current GameObject transformation in World Space.
		/// Based on something like Unity3D's: http://docs.unity3d.com/ScriptReference/Transform-forward.html
		/// Based on something like UnrealEngine 4's: https://docs.unrealengine.com/latest/INT/API/Runtime/Engine/GameFramework/AActor/GetActorForwardVector/index.html
		/// Except it provides access to all directions.
		/// </summary>
		IEngineDirectional Directions { get; }
	}
}
