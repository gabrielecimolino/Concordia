using System;
using System.Collections.Generic;
using PAT.Input.LegacyInputManager;
using UnityEngine;

namespace PAT.Input
{
    public class LegacyUnityAxisSensor : LegacyInputSensor<float>
    {
        public override Dictionary<string, Func<float>> Inputs()
        {
            Dictionary<string, Func<float>> axes = new Dictionary<string, Func<float>>();
            List<string> buttons = new List<string>();
            
            foreach (KeyValuePair<string, InputType> pair in Axes())
            {
                if (pair.Value == InputType.JoystickAxis || pair.Value == InputType.MouseMovement) axes[pair.Key] = () => Mathf.Clamp(UnityEngine.Input.GetAxis(pair.Key), -1f, 1f);
            }

            return axes;
        }
    }
}