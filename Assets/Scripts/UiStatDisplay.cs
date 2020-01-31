#pragma warning disable 0649
using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiStatDisplay : MonoBehaviour
{
    [BoxGroup("Element")] public string variable;

    [BoxGroup("Display")] [SerializeField] [TextArea] string desc;
    //[SerializeField] string prefix;
    //[SerializeField] string suffix;
    [BoxGroup("Ref")] [SerializeField] TextMeshProUGUI description;
    [BoxGroup("Ref")] [SerializeField] TextMeshProUGUI button;
    [BoxGroup("Ref")] [SerializeField] Button buttonComp;
    [BoxGroup("Ref")] [SerializeField] GameObjectVariable tower;
    [BoxGroup("Ref")] [SerializeField] FloatVariable argent;
    [BoxGroup("Ref")] [SerializeField] uiStatDisplayDot dots;
    [BoxGroup("Ref")] [SerializeField] SimpleTooltip simpleTooltip;



    TowerStat stat;

    private void Start()
    {
        buttonComp = GetComponentInChildren<Button>();
        simpleTooltip = GetComponentInChildren<SimpleTooltip>();

        simpleTooltip.infoLeft = desc;

    }
    public void UpdateInterface()
    {
        if (tower.Value != null)
        {
            stat = tower.Value.gameObject.GetComponent<Tower>().stat;
            //description.text = prefix + stat.datas[variable].upgrateLevel + suffix;
            description.text = stat.datas[variable].upgrateLevel.ToString();
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

            Tower tw = tower.Value.gameObject.GetComponent<Tower>();
            stat = tw.stat;
            if (stat.datas[variable].upgrateLevel < stat.datas[variable].upgrateLevelMax)
            {
                if (argent.Value - stat.datas[variable].cost >= 0)
                {
                    stat.datas[variable].upgrateLevel++;
                    argent.Value -= stat.datas[variable].cost;
                    stat.GetValue();

                    tw.UpdateInfo();
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
        description.text = value;
    }
    public void SetValue(int value)
    {
        description.text = value.ToString();
    }
}
