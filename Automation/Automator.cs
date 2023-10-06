using System.Collections.Generic;
using PAT.Plugin;

namespace PAT.Automation
{
    public abstract class Automator<T> : Plugin<T>
    {
        protected Automator(string featureName) : base(AutomationManager.AIInputInterfaceName, featureName) { }

        public override void Initialize(Dictionary<string, PluginInterface> requiredInterfaces, Dictionary<string, PluginInterface> providedInterfaces)
        {
            Initialize(requiredInterfaces[AIManager.AIStateInterfaceName], providedInterfaces[AutomationManager.AIInputInterfaceName]);
        }

        public abstract void Initialize(PluginInterface aiState, PluginInterface aiInputs);
    }
}