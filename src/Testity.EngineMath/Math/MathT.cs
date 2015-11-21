using MiscUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineMath
{
	/// <summary>
	/// A static generic math functionality class for computing.
	/// Based on Unity3D's: http://docs.unity3d.com/ScriptReference/Mathf.html
	/// </summary>
	public static class MathT
	{
		public static TMathType Sqrt<TMathType>(TMathType val)
			where TMathType : struct
		{
			return (TMathType)Convert.ChangeType(Math.Sqrt((double)Convert.ChangeType(val, typeof(double))), typeof(TMathType));
		}

		public static TMathType Max<TMathType>(TMathType val1, TMathType val2)
			where TMathType : struct
		{
			return Operator<TMathType>.GreaterThanOrEqual(val1, val2) ? val1 : val2;
		}

		public static TMathType Min<TMathType>(TMathType val1, TMathType val2)
			where TMathType : struct
		{
			return Operator<TMathType>.LessThanOrEqual(val1, val2) ? val1 : val2;
		}
	}
}
