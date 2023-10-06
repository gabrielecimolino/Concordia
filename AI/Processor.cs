using System.Collections.Generic;
using PAT.Plugin;

namespace PAT.Automation
{
    public abstract class Processor<T> : Plugin<T>
    {
        public Processor(string featureName) : base(AIManager.AIStateInterfaceName, featureName) { }

        public override void Initialize(Dictionary<string, PluginInterface> requiredInterfaces, Dictionary<string, PluginInterface> providedInterfaces)
        {
            Initialize(requiredInterfaces[PATManager.GameStateInterfaceName], providedInterfaces[AIManager.AIStateInterfaceName]);
        }

        public abstract void Initialize(PluginInterface gameState, PluginInterface aiState);
    }
}