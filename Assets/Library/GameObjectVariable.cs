// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

namespace ScriptableVariable.Unite2017.Variables
{
    [CreateAssetMenu]
    public class GameObjectVariable : UnityEngine.ScriptableObject
    {
#if UNITY_EDITOR
        [Multiline]
        public string DeveloperDescription = "";
#endif
        public GameObject Value;

        public void SetValue(GameObject value)
        {
            Value = value;
        }

        public void SetValue(GameObjectVariable value)
        {
            Value = value.Value;
        }
        public void Clear()
        {
            Value = null;
        }
    }
}