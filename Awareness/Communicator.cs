using System.Collections.Generic;
using PAT.Automation;
using PAT.Plugin;

namespace PAT.Awareness
{
    public abstract class Communicator<T> : Plugin<T>
    {
        protected Communicator(string featureName) : base(AwarenessManager.CuesInterfaceName, featureName) { }

        public override void Initialize(Dictionary<string, PluginInterface> requiredInterfaces, Dictionary<string, PluginInterface> providedInterfaces)
        {
            Initialize(requiredInterfaces[AIManager.AIStateInterfaceName], providedInterfaces[AwarenessManager.CuesInterfaceName]);
        }

        public abstract void Initialize(PluginInterface aiState, PluginInterface cues);
    }
}