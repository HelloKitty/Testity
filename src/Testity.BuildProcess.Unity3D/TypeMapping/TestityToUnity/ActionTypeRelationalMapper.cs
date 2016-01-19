using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testity.BuildProcess.Unity3D
{
	public class ActionTypeRelationalMapper : ITypeRelationalMapper
	{
		private Type[] validGenericActionTypes = new Type[] { typeof(Action<>), typeof(Action<,>), typeof(Action<,,>), typeof(Action<,,,>) };

		public Type ResolveMappedType(Type typeToFindRelation)
		{
			//Check to see if it's a handable action type
			if (!isActionType(typeToFindRelation))
				return null;

			//if(typeToFindRelation.IsGenericType)
			return null;
		}

		public bool isActionType(Type t)
		{
			//It must either be Action or Action<...>
			return typeof(Action) == t || (t.IsGenericType && validGenericActionTypes.Contains((t.GetGenericTypeDefinition())));
        }

		private Type FindValidUnityEventType(Type actionType)
		{
			//if(actionType == typeof())
			return null;
		}
	}
}
