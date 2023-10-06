using System.Collections.Generic;

namespace PAT.Plugin
{
    public abstract class Plugin
    {
        public abstract void Initialize(Dictionary<string, PluginInterface> requiredInterfaces, Dictionary<string, PluginInterface> providedInterfaces);
    }

    public abstract class Plugin<T> : Plugin
    {
        public string InterfaceName { get; set; }
        public virtual string FeatureName { get; set; }
        
        public Plugin(string interfaceName, string featureName)
        {
            InterfaceName = interfaceName;
            FeatureName = featureName;
        }
        public abstract T Compute();
    }
}
