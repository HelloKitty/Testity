using MiscUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testity.EngineMath
{
	public static class MathT
	{
		public static TMathType Sqrt<TMathType>(TMathType val)
		{
			return (TMathType)Convert.ChangeType(Math.Sqrt((double)Convert.ChangeType(val, typeof(double))), typeof(TMathType));
		}

		public static TMathType Max<TMathType>(TMathType val1, TMathType val2)
		{
			return Operator<TMathType>.GreaterThanOrEqual(val1, val2) ? val1 : val2;
		}

		public static TMathType Min<TMathType>(TMathType val1, TMathType val2)
		{
			return Operator<TMathType>.LessThanOrEqual(val1, val2) ? val1 : val2;
		}
	}
}
