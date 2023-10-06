using System;
using System.Collections.Generic;
using UnityEngine;

namespace PAT.Configuration
{
    public class InputMenu : MonoBehaviour
    {
        [SerializeField] private GameObject InputPanelPrefab;

        [SerializeField] private Transform _inputList;
        
        private Dictionary<string, Type> _inputs;
        private Dictionary<Type, string[]> _controls;

        void Awake()
        {
            GatherInputs();
        }

        public void GatherInputs()
        {
            _inputs = PATManager.GameInputs();
            _controls = new Dictionary<Type, string[]>();
            
            foreach(string name in _inputs.Keys)
            {
                if (!_controls.ContainsKey(_inputs[name]))
                {
                    _controls[_inputs[name]] = PATManager.PlayerInputs(_inputs[name]);
                }
            }

            foreach (string name in _inputs.Keys)
            {
                ConstructInputPanel(name);
            }
        }

        void ConstructInputPanel(string name)
        {
            GameObject inputPanelGO = Instantiate(InputPanelPrefab, _inputList);
            InputConfigurator configurator = inputPanelGO.GetComponent<InputConfigurator>();

            configurator.Name = name;
            configurator.Controls = _controls[_inputs[name]];
            PATManager.ConfigureInput(name, configurator);
        }
    }
}

