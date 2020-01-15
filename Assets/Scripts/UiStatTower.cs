using NaughtyAttributes;
using ScriptableVariable.Unite2017.Events;
using ScriptableVariable.Unite2017.Variables;
using TMPro;
using UnityEngine;

public class UiStatTower : MonoBehaviour
{

    [BoxGroup("Variable Global")] [SerializeField] FloatVariable argent;
    [BoxGroup("Events")] [SerializeField] GameEvent closeEvent;


    [BoxGroup("Debug")] [SerializeField] TextMeshProUGUI description;

    [BoxGroup("Tour Actuel")] [SerializeField] GameObjectVariable tower;
    [BoxGroup("Tour Actuel")] [SerializeField] [ReadOnly] TowerStat stat;





    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI UiName;
    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI UiSellButton;

    [BoxGroup("Stats")] [SerializeField] UiStatDisplay UiRadius;
    [BoxGroup("Stats")] [SerializeField] UiStatDisplay UiDamage;
    [BoxGroup("Stats")] [SerializeField] UiStatDisplay UiRechargeDuration;



    string text;

    void Start()
    {
        UpdateUi();
    }


    [Button]
    public void UpdateUi()
    {
        if (tower.Value != null)
        {
            stat = tower.Value.gameObject.GetComponent<Tower>().stat;

            #region Debug
            text = "";
            text += "name " + tower.Value.name + "\n";
            text += "Buy Cost " + stat.buyCost + "\n";
            text += "Sell Value " + stat.sellValue + "\n";
            text += "radius " + stat.datas["Radius"].upgrateLevel + "\n";
            text += "damage " + stat.datas["Damage"].upgrateLevel + "\n";
            text += "recharge " + stat.datas["ReloadSpeed"].upgrateLevel + "\n";
            text += "ZoneAttack " + stat.isZoneAttack + "\n";
            #endregion

            description.text = text;

            UiName.text = tower.Value.name;



            UiRadius.SetValue(stat.datas[TowerStat.listData.Radius.ToString()].upgrateLevel);
            UiDamage.SetValue(stat.datas[TowerStat.listData.Damage.ToString()].upgrateLevel);
            UiRechargeDuration.SetValue(stat.datas[TowerStat.listData.ReloadSpeed.ToString()].upgrateLevel);

            UiSellButton.text = "sell:" + stat.sellValue;
        }
        else
        {
            Debug.LogError("Aucune tour n'est selectionnée");
            closeEvent.Raise();
        }
    }



    public void sellTower()
    {
        Tower tw = tower.Value.GetComponent<Tower>();



        argent.ApplyChange(stat.GetValue());

        tw.sellTower();
        closeEvent.Raise();
    }

}
