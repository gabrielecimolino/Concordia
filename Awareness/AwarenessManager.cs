using PAT.Plugin;

namespace PAT.Awareness
{
    public class AwarenessManager : PluginManager
    {
        public static string CuesInterfaceName = "Cues";
        
        public AwarenessManager() : base()
        {
            _providedInterfaces[CuesInterfaceName] = new PluginInterface();
        }
    }
}
