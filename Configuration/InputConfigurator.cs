using System;
using System.Collections.Generic;
using System.Linq;
using PAT.Configuration.Configurators;
using UnityEngine;
using UnityEngine.UI;

namespace PAT.Configuration
{
    public class InputConfigurator : MonoBehaviour
    {
        [SerializeField] private GameObject _checkboxPrefab;
        [SerializeField] private GameObject _dropdownPrefab;
        [SerializeField] private GameObject _radioPrefab;
        [SerializeField] private GameObject _sliderPrefab;

        [SerializeField] private Text _name;
        [SerializeField] private Dropdown _controls;
        [SerializeField] private Transform _assistance;
        
        public string Name
        {
            set { _name.text = value; }
        }

        public string[] Controls
        {
            set
            {
                List<Dropdown.OptionData> options = new List<Dropdown.OptionData>() {new Dropdown.OptionData("")};
                options.AddRange(value.Select(option => new Dropdown.OptionData(option)).ToList());

                _controls.options = options;
            }
        }

        public void SetControl(Dropdown value)
        {
            if(value.value == 0) PATManager.UnassignInput(_name.text);
            else PATManager.AssignInput(_name.text, _controls.options[value.value].text);
        }
        
        public void AddCheckbox(ConfigurationCheckbox checkbox)
        {
            GameObject checkboxGO = Instantiate(_checkboxPrefab, _assistance);
            
            CheckboxConfigurator configurator = checkboxGO.GetComponent<CheckboxConfigurator>();
            configurator.SetConfiguration(checkbox);
        }

        public void AddDropdown<T>(ConfigurationDropdown<T> dropdown) where T : Enum
        {
            GameObject dropdownGO = Instantiate(_dropdownPrefab, _assistance);

            DropdownConfigurator configurator = dropdownGO.GetComponent<DropdownConfigurator>();
            configurator.SetConfiguration(dropdown);
        }

        public void AddRadio<T>(ConfigurationDropdown<T> dropdown, Dictionary<string, Sprite> sprites) where T : Enum
        {
            GameObject radioGO = Instantiate(_radioPrefab, _assistance);

            RadioConfigurator configurator = radioGO.GetComponent<RadioConfigurator>();
            configurator.SetConfiguration(dropdown);
            configurator.SetButtons(sprites);
        }
        
        public void AddSlider(ConfigurationSlider<float> slider, Sprite leftIcon, Sprite rightIcon)
        {
            GameObject sliderGO = Instantiate(_sliderPrefab, _assistance);

            SliderConfigurator configurator = sliderGO.GetComponent<SliderConfigurator>();
            configurator.SetConfiguration(slider);
            configurator.SetIcons(leftIcon, rightIcon);
        }
    }
}