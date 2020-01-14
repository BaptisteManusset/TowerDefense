using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using TMPro;
using UnityEngine;

public class UiStatTower : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] GameObjectVariable tower;
    [SerializeField] TextMeshProUGUI UiName;
    [SerializeField] UiStatDisplay UiRadius;
    [SerializeField] UiStatDisplay UiDamage;
    [SerializeField] UiStatDisplay UiRechargeDuration;
    TowerStat stat;
    string text;
    [Button]
    public void UpdateUi()
    {
        if (tower.Value != null)
        {
            stat = tower.Value.gameObject.GetComponent<Tower>().stat;
            text = "";
            text += "name " + tower.Value.name + "\n";
            text += "radius " + stat.radius + "\n";
            text += "damage " + stat.damage + "\n";
            text += "recharge " + stat.reloadSpeed + "\n";
            text += "ZoneAttack " + stat.isZoneAttack + "\n";
            description.text = text;

            UiName.name = stat.name;
            UiRadius.SetValue(stat.radius);
            UiDamage.SetValue(stat.damage);
            UiRechargeDuration.SetValue(stat.reloadSpeed);
        }
        else
        {
            Debug.LogError("Aucune tour n'est selectionnée");
        }
    }


    public void IncrementRadius()
    {
        stat.radius++;
    }
    public void IncrementDamage()
    {
        stat.damage++;
    }
    public void IncrementReload()
    {
        stat.reloadSpeed++;
    }
}
