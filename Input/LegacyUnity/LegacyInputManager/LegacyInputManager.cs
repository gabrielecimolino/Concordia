using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PAT.Input.LegacyInputManager
{
    public enum InputType
    {
        KeyOrMouseButton,
        MouseMovement,
        JoystickAxis,
    }
    
    public class LegacyInputManager : MonoBehaviour
    {
        static LegacyInputManager _instance;
        
        [SerializeField] private List<string> names;
        [SerializeField] private List<InputType> types;
        
        public static Dictionary<string, InputType> Axes
        {
            get
            {
                Dictionary<string, InputType> axes = new Dictionary<string, InputType>();

                for (int i = 0; i < _instance.names.Count; i++)
                {
                    axes[_instance.names[i]] = _instance.types[i];
                }

                return axes;
            }
        }

        void Start()
        {
            _instance = this;
        }
        
        private void OnValidate()
        {
            Dictionary<string, InputType>axes = GetAxes();
            names = axes.Keys.ToList();
            types = axes.Values.ToList();
        }
        
        // Based on https://discussions.unity.com/t/get-array-of-all-input-manager-axes/136679/2
        protected Dictionary<string, InputType> GetAxes()
        {
            #if UNITY_EDITOR
                UnityEngine.Object obj = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
                SerializedObject inputManager = new SerializedObject(obj);
                SerializedProperty axes = inputManager.FindProperty("m_Axes");

                Dictionary<string, InputType> axesDict = new Dictionary<string, InputType>();

                foreach (SerializedProperty axis in axes)
                {
                    axesDict[axis.FindPropertyRelative("m_Name").stringValue] = (InputType) axis.FindPropertyRelative("type").intValue;
                }

                return axesDict;
            #endif

            return Axes;
        }
    }
}