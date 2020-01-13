// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableVariable.Unite2017.Sets
{
    public class ThingMonitor : MonoBehaviour
    {
        [SerializeField] ThingRuntimeSet Set;

        [SerializeField] TextMeshProUGUI Text;

        [SerializeField] int previousCount = -1;

        [SerializeField] string prefix;
        [SerializeField] string suffix;

        private void OnEnable()
        {
            UpdateText();
        }

        private void Update()
        {
            if (previousCount != Set.Items.Count)
            {
                UpdateText();
                previousCount = Set.Items.Count;
            }
        }

        public void UpdateText()
        {
            Text.text = prefix + Set.Items.Count + suffix;
        }
    }
}