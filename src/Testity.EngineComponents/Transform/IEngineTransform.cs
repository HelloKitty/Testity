using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineMath;

namespace Testity.EngineComponents
{
	/// <summary>
	/// A transformation data interface.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Transform.html
	/// </summary>
	[EngineComponentInterface]
	public interface IEngineTransform
	{
		/// <summary>
		/// Position of the transform in world space.
		/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Transform-position.html
		/// </summary>
		Vector3<float> Position { get; }

		/// <summary>
		/// Position of the transform relative to its parent
		/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Transform-localPosition.html
		/// </summary>
		Vector3<float> LocalPosition { get; }

		/// <summary>
		/// Position of the transform relative to its parent
		/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Transform-localScale.html
		/// </summary>
		Vector3<float> LocalScale { get; }
	}
}
