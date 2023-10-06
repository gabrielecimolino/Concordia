using System;
using System.Collections.Generic;
using System.Linq;
using PAT.Configuration;
using PAT.Input;
using PAT.Plugin;

namespace PAT.Mediation
{
    public class MediationManager : PluginManager
    {
        public static string GameInterfaceName = "Game Input";
        
        protected Dictionary<string, IAssignable> _mediationPlugins;

        public MediationManager() : base()
        {
            _providedInterfaces[GameInterfaceName] = new PluginInterface();
            
            _mediationPlugins = new Dictionary<string, IAssignable>();
        }

        public void CreateInput<T>(string inputName, Mediator<T> mediator)
        {
            MediationPlugin<T> plugin = new MediationPlugin<T>(inputName, mediator);
            RegisterPlugin(plugin);
            plugin.Initialize(_requiredInterfaces, _providedInterfaces);

            _mediationPlugins[inputName] = plugin;
        }
        
        public void AssignInput(string inputName, string playerInputName)
        {
            _mediationPlugins[inputName].Assign(_requiredInterfaces[InputManager.PlayerInterfaceName][playerInputName]);
        }

        public void UnassignInput(string inputName)
        {
            _mediationPlugins[inputName].Unassign();
        }

        public void ConfigureInput(string inputName, InputConfigurator configurator)
        {
            _mediationPlugins[inputName].Configure(configurator);
        }

        public Dictionary<string, Type> GameInputs()
        {
            return new Dictionary<string, Type>(_mediationPlugins.Select(pair =>
                new KeyValuePair<string, Type>(pair.Key, pair.Value.GetType())));
        }

        public string[] PlayerInputs(Type type)
        {
            return _requiredInterfaces[InputManager.PlayerInterfaceName].Where(pair => pair.Value.GetType() == type).Select(pair => pair.Key).ToArray();
        }
    }
}