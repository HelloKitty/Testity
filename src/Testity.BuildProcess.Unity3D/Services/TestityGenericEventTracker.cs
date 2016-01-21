using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class TestityGenericEventTracker
	{
		private readonly object syncObj = new object();

		private readonly Dictionary<string, IEnumerable<Type>> eventNameToGenericArgsMap;

		public IEnumerable<KeyValuePair<string, IEnumerable<Type>>> GenericTestityEventData
		{
			get { return eventNameToGenericArgsMap; }
		}

		public TestityGenericEventTracker()
		{
			eventNameToGenericArgsMap = new Dictionary<string, IEnumerable<Type>>();
        }

		public void Register(string key, IEnumerable<Type> value)
		{
			//very possible this will be called during a threated build step/task
			lock(syncObj)
			{

				if (eventNameToGenericArgsMap.ContainsKey(key))
					return;

				eventNameToGenericArgsMap[key] = value;
			}
		}
	}
}
