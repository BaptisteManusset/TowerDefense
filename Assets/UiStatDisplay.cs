using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiStatDisplay : MonoBehaviour
{
    [SerializeField] string prefix;
    [SerializeField] string suffix;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] GameObjectVariable tower;

    public void SetValue(string value)
    {
        description.text = prefix + value + suffix;
    }
    public void SetValue(int value)
    {
        description.text = prefix + value + suffix;
    }
}
