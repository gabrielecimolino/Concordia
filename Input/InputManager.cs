using System;
using System.Collections.Generic;
using PAT.Plugin;

namespace PAT.Input
{
    public class InputManager : PluginManager
    {
        public static string PlayerInterfaceName = "Player Input";

        public InputManager() : base()
        {
            _providedInterfaces[PlayerInterfaceName] = new PluginInterface();
        }

        public void RegisterSensor<T>(Sensor<T> sensor)
        {
            foreach (KeyValuePair<string, Func<T>> pair in sensor.Inputs())
            {
                RegisterPlugin(new InputPlugin<T>(pair.Key, pair.Value));
            }
        }
    }
}
