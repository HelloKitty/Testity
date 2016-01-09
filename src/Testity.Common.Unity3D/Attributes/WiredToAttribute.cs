using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Testity.Common.Unity3D
{
	/// <summary>
	/// Metadata marker for fields that indicate what field/property the target field
	/// should initialize/wire its values to.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)] //should only go on serialized Unity fields
	public class WiredToAttribute : Attribute
	{
		/// <summary>
		/// Indicates the reflection type this field is wired to.
		/// </summary>
		public readonly MemberTypes WiredMemberType;

		/// <summary>
		/// Indicates the name of the member this field is wired to.
		/// </summary>
		public readonly string WiredMemberName;

		public WiredToAttribute(MemberTypes memberType, string name)
		{
			WiredMemberType = memberType;
			WiredMemberName = name;
        }
	}
}
