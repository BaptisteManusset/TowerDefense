// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace ScriptableVariable.Unite2017.Variables
{
    [Serializable]
    public class GameObjectReference
    {
        public bool UseConstant = true;
        public GameObject ConstantValue;
        public GameObjectVariable Variable;

        public GameObjectReference()
        { }

        public GameObjectReference(GameObject value)
        {
            UseConstant = true;
            ConstantValue = value;
        }

        public bool Value {
            get { return UseConstant ? ConstantValue : Variable.Value; }
        }

        public static implicit operator bool(GameObjectReference reference)
        {
            return reference.Value;
        }
    }
}