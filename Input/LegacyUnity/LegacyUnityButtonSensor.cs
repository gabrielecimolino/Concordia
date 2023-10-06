using System;
using System.Collections.Generic;
using PAT.Input.LegacyInputManager;
using UnityEngine;

namespace PAT.Input
{
    public class LegacyUnityButtonSensor : LegacyInputSensor<bool>
    {
        public override Dictionary<string, Func<bool>> Inputs()
        {
            Dictionary<string, Func<bool>> buttons = new Dictionary<string, Func<bool>>();

            foreach (KeyValuePair<string, InputType> pair in Axes())
            {
                if (pair.Value == InputType.KeyOrMouseButton) buttons[pair.Key] = () => UnityEngine.Input.GetButton(pair.Key);
                if (pair.Value == InputType.JoystickAxis)
                {
                    buttons[pair.Key + "+"] = () => UnityEngine.Input.GetAxis(pair.Key) > 0f;
                    buttons[pair.Key + "-"] = () => UnityEngine.Input.GetAxis(pair.Key) < 0f;
                }
            }

            foreach (KeyCode key in Keys())
            {
                buttons[key.ToString()] = () => UnityEngine.Input.GetKey(key);
            }

            return buttons;
        }
    }
}