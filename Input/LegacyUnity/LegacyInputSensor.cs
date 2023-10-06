using System;
using System.Collections.Generic;
using PAT.Input.LegacyInputManager;
using UnityEngine;

namespace PAT.Input
{
    public abstract class LegacyInputSensor<T> : Sensor<T>
    {
        protected Dictionary<string, InputType> Axes()
        {
            return PAT.Input.LegacyInputManager.LegacyInputManager.Axes;
        }

        protected KeyCode[] Keys()
        {
            return (KeyCode[]) Enum.GetValues(typeof(KeyCode));
        }
    }
}