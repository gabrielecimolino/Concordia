using UnityEngine;
using UnityEngine.UI;

namespace PAT.Configuration.Configurators
{
    public class SliderConfigurator : Configurator<ConfigurationSlider<float>>
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _leftIcon;
        [SerializeField] private Image _rightIcon;

        private ConfigurationSlider<float> _configuration;

        public void Update()
        {
            _slider.value = _configuration.Value;
        }

        public override void SetConfiguration(ConfigurationSlider<float> configuration)
        {
            base.SetConfiguration(configuration);

            _configuration = configuration;
            _slider.value = configuration.Value;
        }

        public void SetIcons(Sprite left, Sprite right)
        {
            _leftIcon.sprite = left;
            _rightIcon.sprite = right;
        }

        public void ChangeConfiguration(float value)
        {
            _configuration.Value = value;
        }
    }
}