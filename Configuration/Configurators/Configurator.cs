using UnityEngine;
using UnityEngine.UI;

namespace PAT.Configuration.Configurators
{
    public class Configurator<T> : MonoBehaviour where T : Configuration
    {
        [SerializeField] private Text _label;
        
        public virtual void SetConfiguration(T configuration)
        {
            _label.text = configuration.Name;
        }
    }
}