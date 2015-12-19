using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess
{
	public interface IMemberImplementationProvider
	{
		string MemberName { get; }

		Type MemberType { get; }

		MemberImplementationModifier Modifiers { get; }

		IEnumerable<Attribute> Attributes { get; }
	}
}
