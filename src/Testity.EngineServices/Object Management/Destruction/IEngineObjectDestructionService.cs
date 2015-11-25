using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices
{

	/// <summary>
	/// Implementer provides functionality for destroying and cleaning up of <see cref="IEngineObject"/>s.
	/// Based loosely on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Destroy.html
	/// </summary>
	[EngineServiceInterface]
	public interface IEngineObjectDestructionService
	{
		/// <summary>
		/// Destroys the <see cref="IEngineObject"/> requested.
		/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Destroy.html
		/// </summary>
		/// <param name="toDestroy"><see cref="IEngineObject"/> to destroy.</param>
		/// <returns>Indicates if destruction was sucessful.</returns>
		bool Destroy(IEngineObject toDestroy);

		/// <summary>
		/// Destroys the <see cref="IEngineObject"/> requested approximately time seconds after this call.
		/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Object.Destroy.html overload
		/// <param name="time">Time in seconds to approximately delay the destruction of the object.</param>
		/// <param name="toDestroy"><see cref="IEngineObject"/> to destroy.</param>
		/// </summary>
		/// <returns>Indicates if destruction was sucessful.</returns>
		bool DestroyDelayed(IEngineObject toDestroy, float time);
	}
}
