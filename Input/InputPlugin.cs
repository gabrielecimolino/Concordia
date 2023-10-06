using System;
using System.Collections.Generic;
using PAT.Plugin;

namespace PAT.Input
{
    public class InputPlugin<T> : Plugin<T>
    {
        private Func<T> _compute;

        public InputPlugin(string featureName, Func<T> compute) : base(InputManager.PlayerInterfaceName, featureName)
        {
            _compute = compute;
        }
        
        public override void Initialize(Dictionary<string, PluginInterface> requiredInterfaces, Dictionary<string, PluginInterface> providedInterfaces) { }

        public override T Compute()
        {
            return _compute();
        }
    }
}