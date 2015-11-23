using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineComponents
{
	/// <summary>
	/// Implementer is an <see cref="IEngineComponent"/> that is a custom and enable/disable-able script component.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Behaviour.html
	/// </summary>
	public interface IEngineScriptComponent : IEngineComponent, IEngineActivatable
	{
		/// <summary>
		/// Indicates if the <see cref="IEngineScriptComponent"/> is both active and enable event has been processed.
		/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Behaviour-isActiveAndEnabled.html
		/// </summary>
		bool FullyEnabled { get; }
	}
}
