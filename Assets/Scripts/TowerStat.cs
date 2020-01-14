using NaughtyAttributes;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerStat : ScriptableObject
{
  //[Space(20)] public int radius;
  //public int radiusUpgradeCost;

  [Space(20)] public int damage;
  //public int damageUpgradeCost;

  [Space(20)] public int reloadSpeed;
  //public int reloadSpeedUpgradeCost;

  [Space(20)] public bool isZoneAttack;




  [System.Serializable]
  public class Data
  {
    public string id;
    public int value = 0;
    public int cost = 10;
    public int upgrateLevel = 0;
  }

  [System.Serializable]
  public class GenericDictionary : SerializableDictionaryBase<string, Data> { }

  [SerializeField, ID("id")]
  public GenericDictionary datas;


  public enum listData
  {
    Radius,
    Damage,
    ReloadSpeed
  }



  [Button]
  void Init()
  {
    datas.Clear();
    //Debug.Log("init");
    //Data d = new Data();

    //d.id = "Radius";
    //datas.Add(d.id, d);

    //d.id = "Damage";
    //datas.Add(d.id, d);

    //d.id = "ReloadSpeed";
    //datas.Add(d.id, d);
  }
}
