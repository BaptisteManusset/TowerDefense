using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField] GameObjectVariable tour;
    [SerializeField] GameObject tourToSell;

    [SerializeField] FloatReference argent;

    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiName;
    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiRadius;
    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiPower;
    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiReloadPower;
    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiCout;

    void Awake()
    {
        GameObject t = tour.Value.gameObject;
        TowerStat stat = t.GetComponent<Tower>().statDefault;
        uiName.text = t.name;
        uiRadius.text = stat.datas["Radius"].upgrateLevel.ToString();
        uiPower.text = stat.datas["Damage"].upgrateLevel.ToString();
        uiReloadPower.text = stat.datas["ReloadSpeed"].upgrateLevel.ToString();
        uiCout.text = stat.buyCost + "#";
    }
    public void BuyTower()
    {
        //if (argent.Value >= 0)
        //{
        tour.Value = tourToSell;
        //}
    }
}
