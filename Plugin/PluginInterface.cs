using System.Collections.Generic;

namespace PAT.Plugin
{
    public class PluginInterface<T> : Dictionary<string, T> where T : Feature { }

    public class PluginInterface : PluginInterface<Feature>
    {
        public void Refresh()
        {
            foreach (Feature feature in Values) feature.Fresh = false;
        }
    }
}
