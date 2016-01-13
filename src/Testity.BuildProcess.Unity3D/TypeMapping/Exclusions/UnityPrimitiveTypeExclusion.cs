using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class UnityPrimitiveTypeExclusion : IEnumerable<Type>
	{
		private readonly Type[] excludedPrimitiveTypes
			= new Type[] { typeof(DateTime), typeof(IntPtr), typeof(UIntPtr) };

		public UnityPrimitiveTypeExclusion()
		{
			//do nothing
		}

		public IEnumerator<Type> GetEnumerator()
		{
			return excludedPrimitiveTypes.AsEnumerable<Type>().GetEnumerator();
        }

		IEnumerator IEnumerable.GetEnumerator()
		{
			return excludedPrimitiveTypes.GetEnumerator();
        }
	}
}
