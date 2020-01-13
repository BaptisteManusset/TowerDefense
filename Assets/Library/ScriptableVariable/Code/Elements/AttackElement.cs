// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

namespace ScriptableVariable.Unite2017.Elements
{
    [CreateAssetMenu]
    public class AttackElement : UnityEngine.ScriptableObject
    {
        [Tooltip("The elements that are defeated by this element.")]
        public List<AttackElement> DefeatedElements = new List<AttackElement>();
    }
}