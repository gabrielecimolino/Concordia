using PAT.Plugin;

namespace PAT.Automation
{
    public class AutomationManager : PluginManager
    {
        public static string AIInputInterfaceName = "AI Input";
        
        public AutomationManager() : base()
        {
            _providedInterfaces[AIInputInterfaceName] = new PluginInterface();
        }
        
    }
}
