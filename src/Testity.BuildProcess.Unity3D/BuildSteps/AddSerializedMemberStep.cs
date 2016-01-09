using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class AddSerializedMemberStep : ITestityBuildStep
	{
		private readonly Type TypeToParse;

		public AddSerializedMemberStep(Type typeToParse)
		{
			TypeToParse = typeToParse;
        }

		public void Process(IClassBuilder builder)
		{
			//handle serialized properties first
			IEnumerable<PropertyInfo> propertiesToHandle = SerializedMemberParser<PropertyInfo>.Parse(TypeToParse);
		}
	}
}
