using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using TMPro;
using UnityEngine;

public class UiStatTower : MonoBehaviour
{
  [BoxGroup("Debug")] [SerializeField] TextMeshProUGUI description;
  [BoxGroup("Variable")] [SerializeField] GameObjectVariable tower;
  [BoxGroup("UI")] [SerializeField] TextMeshProUGUI UiName;
  [BoxGroup("UI")] [SerializeField] UiStatDisplay UiRadius;
  [BoxGroup("UI")] [SerializeField] UiStatDisplay UiDamage;
  [BoxGroup("UI")] [SerializeField] UiStatDisplay UiRechargeDuration;



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
      text += "radius " + stat.datas["Radius"].value + "\n";
      text += "damage " + stat.datas["Damage"].value + "\n";
      text += "recharge " + stat.datas["ReloadSpeed"].value + "\n";
      text += "ZoneAttack " + stat.isZoneAttack + "\n";
      description.text = text;

      UiName.name = stat.name;
      Debug.Log("");
      Debug.Log("");
      Debug.Log("");
      Debug.Log(UiRadius);
      Debug.Log(TowerStat.listData.Radius.ToString());
      Debug.Log(stat.datas[TowerStat.listData.Radius.ToString()].value);
      UiRadius.SetValue(
        stat.datas[TowerStat.listData.Radius.ToString()].value);
      UiDamage.SetValue(
        stat.datas[TowerStat.listData.Damage.ToString()].value);
      UiRechargeDuration.SetValue(
        stat.datas[TowerStat.listData.ReloadSpeed.ToString()].value);
    } else
    {
      Debug.LogError("Aucune tour n'est selectionnée");
    }
  }


}
