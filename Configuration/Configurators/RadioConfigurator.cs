using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PAT.Configuration.Configurators
{
    public class RadioConfigurator : Configurator<ConfigurationDropdown>
    {
        [SerializeField] private GameObject ButtonPrefab;
        [SerializeField] private Transform ButtonsTransform;

        [SerializeField] private Color EnabledColor;
        [SerializeField] private Color DisabledColor;

        private List<Image> _buttonImages;
        private ConfigurationDropdown _configuration;

        public override void SetConfiguration(ConfigurationDropdown configuration)
        {
            base.SetConfiguration(configuration);

            _configuration = configuration;
        }

        public void SetButtons(Dictionary<string, Sprite> sprites)
        {
            _buttonImages = new List<Image>();
            
            foreach (string option in _configuration.Options())
            {
                GameObject buttonGO = Instantiate(ButtonPrefab, ButtonsTransform);
                Button button = buttonGO.GetComponentInChildren<Button>();
                Image image = buttonGO.GetComponentInChildren<Image>();
                Text text = buttonGO.GetComponentInChildren<Text>();

                text.text = option;
                image.sprite = sprites[option];
                button.onClick.AddListener(() => ChangeConfiguration(_configuration.OptionValue(option)));
                
                _buttonImages.Add(image);
            }
        }

        public void ChangeConfiguration(int value)
        {
            _configuration.Value = value;
        }

        public void Update()
        {
            for (int i = 0; i < _buttonImages.Count; i++)
            {
                _buttonImages[i].color = i == _configuration.Value ? EnabledColor : DisabledColor;
            }
        }
    }
}