using System.Collections.Generic;

namespace PAT.Plugin
{
    public class PluginManager
    {
        protected Dictionary<string, PluginInterface> _requiredInterfaces;
        protected Dictionary<string, PluginInterface> _providedInterfaces;

        protected List<Plugin> _plugins;

        public PluginInterface this[string name]
        {
            get { return _providedInterfaces[name]; }
        }
        
        public PluginManager()
        {
            _requiredInterfaces = new Dictionary<string, PluginInterface>();
            _providedInterfaces = new Dictionary<string, PluginInterface>();
            _plugins = new List<Plugin>();
        }

        public void Refresh()
        {
            foreach(PluginInterface pluginInterface in _providedInterfaces.Values) pluginInterface.Refresh();
        }

        public void RegisterPlugin<T>(Plugin<T> plugin)
        {
            _providedInterfaces[plugin.InterfaceName][plugin.FeatureName] = new Feature<T>(plugin.Compute);
            _plugins.Add(plugin);
        }

        public void InitializePlugins()
        {
            foreach(Plugin plugin in _plugins) plugin.Initialize(_requiredInterfaces, _providedInterfaces);
        }
        
        public void ConnectRequiredInterface(string interfaceName, PluginInterface pluginInteface)
        {
            _requiredInterfaces[interfaceName] = pluginInteface;
        }
    }
}
