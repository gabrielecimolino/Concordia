using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace PAT.Configuration.Configurators
{
    public class DropdownConfigurator : Configurator<ConfigurationDropdown>
    {
        [SerializeField] private Dropdown _dropdown;

        private ConfigurationDropdown _configuration;

        public override void SetConfiguration(ConfigurationDropdown configuration)
        {
            base.SetConfiguration(configuration);

            _configuration = configuration;

            _dropdown.options = _configuration.Options().Select(option => new Dropdown.OptionData(option)).ToList();
            _dropdown.value = configuration.Value;
        }

        public void ChangeConfiguration(int value)
        {
            _configuration.Value = value;
        }
    }
}