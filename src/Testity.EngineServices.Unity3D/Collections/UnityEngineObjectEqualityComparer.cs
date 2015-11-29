using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices.Unity3D
{
	//Not needed.
	/*/// <summary>
	/// Provides explict equality for underlying <see cref="UnityEngine.Object"/>s behind the interface. Requires <see cref="IEngineObject"/> equality to be implemeneted
	/// properly.
	/// </summary>
	public class UnityEngineObjectEqualityComparer : IEqualityComparer<IEngineObject>
	{
		/// <summary>
		/// This should indicate if the underlying <see cref="UnityEngine.Object"/> are equal.
		/// </summary>
		/// <param name="x">Instance one.</param>
		/// <param name="y">Instance two.</param>
		/// <returns>Indicates if both adapters have the same underlying <see cref="UnityEngine.Object"/> instances.</returns>
		public bool Equals(IEngineObject x, IEngineObject y)
		{
			//if they're both null then we say true I guess. Else we say false
			//It's to be decided if this is the the Unity functionality. We have to test it.
			if (x == null || y == null)
				if (x == null && y == null)
					return true;
				else
					return false;

			return x.Equals(y);
		}

		/// <summary>
		/// Hash code.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>Hash code.</returns>
		public int GetHashCode(IEngineObject obj)
		{
			return obj.GetHashCode();
		}
	}*/
}
