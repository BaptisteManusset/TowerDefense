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
    [SerializeField] TextMeshProUGUI button;
    [SerializeField] GameObjectVariable tower;
    [SerializeField] FloatVariable argent;


    public string variable;
    TowerStat stat;


    public void UpdateInterface()
    {
        if (tower.Value != null)
        {
            stat = tower.Value.gameObject.GetComponent<Tower>().stat;
            description.text = prefix + stat.datas[variable].upgrateLevel + suffix;
            button.text = stat.datas[variable].cost + "#";

        }
    }
    public void IncrementValue()
    {
        if (tower.Value != null)
        {
            stat = tower.Value.gameObject.GetComponent<Tower>().stat;
            if (argent.Value - stat.datas[variable].cost >= 0)
            {
                stat.datas[variable].upgrateLevel++;
                argent.Value -= stat.datas[variable].cost;
                stat.GetValue();
            }
        }
    }



    public void SetValue(string value)
    {
        description.text = prefix + value + suffix;
    }
    public void SetValue(int value)
    {
        description.text = prefix + value + suffix;
    }
}
