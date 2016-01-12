using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fasterflect;
using Testity.Common;
using Testity.EngineComponents;

namespace Testity.BuildProcess.Unity3D
{
	public class EngineObjectTypeRelationalMapper : ITypeRelationalMapper
	{
		public Type ResolveMappedType(Type typeToFindRelation)
		{
			//If this isn't an IEngineObject then we're unable to map it
			if (!typeof(IEngineObject).IsAssignableFrom(typeToFindRelation))
				return null;

			//We need to find the [EngineSerializableMapsToType] attribute that is for UnityEngine
			EngineSerializableMapsToTypeAttribute mapInfo = typeToFindRelation.Attributes<EngineSerializableMapsToTypeAttribute>()
				.FirstOrDefault(x => x.EngineType == EngineType.Unity3D);

			//Possible that we don't have this Engine type setup it.
			//Though this is unlikely. If an IEngineObject exists it's very likely its been mapped with metadata
			if (mapInfo == null)
				throw new InvalidOperationException("Unable to handle " + nameof(IEngineObject) + " type: " + typeToFindRelation.ToString());

			return mapInfo.ConcreteEngineType;
        }
	}
}
