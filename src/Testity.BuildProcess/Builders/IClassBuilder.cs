using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess
{
	public interface IClassBuilder
	{
		void AddClassField(IMemberImplementationProvider implementationProvider);

		void AddMemberMethod(IMemberImplementationProvider implementationProvider, IBlockBodyProvider blockProvider, IParameterImplementationProvider parametersProvider = null);

		void AddBaseClass<TClassType>()
			where TClassType : class;
    }
}
