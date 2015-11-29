using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Testity.EngineComponents;

namespace Testity.EngineServices.Unity3D
{
	public interface ITestityBehaviour<out TScriptComponentType>
		where TScriptComponentType : EngineScriptComponent
    {
		TScriptComponentType ScriptComponent { get; }
	}
}
