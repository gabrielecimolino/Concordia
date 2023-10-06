using UnityEngine;
using UnityEngine.UI;

namespace PAT.Configuration.Configurators
{
    public class CheckboxConfigurator : Configurator<ConfigurationCheckbox>
    {
        [SerializeField] private Toggle _toggle;

        private ConfigurationCheckbox _checkbox;
        
        public override void SetConfiguration(ConfigurationCheckbox checkbox)
        {
            base.SetConfiguration(checkbox);
            
            _checkbox = checkbox;
            _toggle.isOn = checkbox.Value;
        }

        public void ChangeConfiguration(bool value)
        {
            _checkbox.Value = value;
        }
    }
}