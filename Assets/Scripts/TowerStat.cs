using NaughtyAttributes;
using RotaryHeart.Lib.SerializableDictionary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerStat : ScriptableObject
{
  /*
   * Radius
   * Damage
   * ReloadSpeed
   */


  [Space(20)] public bool isZoneAttack;
  public float sellValue;
  public float buyCost;
  [TextArea]
  public string description;



  [System.Serializable]
  public class Data
  {
    public string id;
    public int value = 0;
    public int cost = 10;
    public int upgrateLevel = 0;
    public int upgrateLevelMax = 6;
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
  public void Init()
  {
    GetValue();
  }

  public float GetValue()
  {
    sellValue = buyCost;

    sellValue += datas["Radius"].cost * datas["Radius"].upgrateLevel;
    sellValue += datas["Damage"].cost * datas["Damage"].upgrateLevel;
    sellValue += datas["ReloadSpeed"].cost * datas["ReloadSpeed"].upgrateLevel;
    sellValue = Mathf.Ceil(sellValue / 4);
    return sellValue;
  }
}
