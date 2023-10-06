using PAT.Plugin;

namespace PAT.Automation
{
    public class AIManager : PluginManager
    {
        public static string AIStateInterfaceName = "AI State";
        
        public AIManager() : base()
        {
            _providedInterfaces[AIStateInterfaceName] = new PluginInterface();
        }
        
    }
}