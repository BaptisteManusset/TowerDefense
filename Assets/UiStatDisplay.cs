using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiStatDisplay : MonoBehaviour
{
    [SerializeField] string prefix;
    [SerializeField] string suffix;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI button;
    [SerializeField] Button buttonComp;
    [SerializeField] GameObjectVariable tower;
    [SerializeField] FloatVariable argent;
    [SerializeField] uiStatDisplayDot dots;


    public string variable;
    TowerStat stat;

    private void Start()
    {
        buttonComp = GetComponentInChildren<Button>();
    }
    public void UpdateInterface()
    {
        if (tower.Value != null)
        {
            stat = tower.Value.gameObject.GetComponent<Tower>().stat;
            description.text = prefix + stat.datas[variable].upgrateLevel + suffix;
            button.text = stat.datas[variable].cost + "#";
            dots.UpdateUi(stat.datas[variable].upgrateLevel, variable);


            if (stat.datas[variable].upgrateLevel >= stat.datas[variable].upgrateLevelMax)
            {
                buttonComp.interactable = false;
            }
            else
            {
                buttonComp.interactable = true;
            }
        }
    }
    public void IncrementValue()
    {
        if (tower.Value != null)
        {
            stat = tower.Value.gameObject.GetComponent<Tower>().stat;

            if (stat.datas[variable].upgrateLevel < stat.datas[variable].upgrateLevelMax)
            {
                if (argent.Value - stat.datas[variable].cost >= 0)
                {
                    stat.datas[variable].upgrateLevel++;
                    argent.Value -= stat.datas[variable].cost;
                    stat.GetValue();
                }
            }
            else
            {
                buttonComp.interactable = false;
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
