using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] GameObjectVariable tour;
    [SerializeField] GameObject tourToSell;

    [SerializeField] FloatReference argent;

    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiName;


    //[BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiRadius;


    [BoxGroup("UI")] [SerializeField] uiStatDisplayDot uiRadiusAdv;
    [BoxGroup("UI")] [SerializeField] uiStatDisplayDot uiDamageAdv;
    [BoxGroup("UI")] [SerializeField] uiStatDisplayDot uiReloadSpeedAdv;


    //[BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiPower;


    //[BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiReloadPower;


    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiDescription;


    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiCout;
    [BoxGroup("UI")] [SerializeField] Image uiZone;

    void Awake()
    {
        tourToSell = null;

    }
    public void BuyTower()
    {
        tour.Value = tourToSell;
    }

    public void SetObj(GameObject obj)
    {
        tourToSell = obj;
        UpdateUi();
    }

    public void UpdateUi()
    {



        TowerStat stat = tourToSell.GetComponent<Tower>().statDefault;
        uiName.text = tourToSell.name;


        uiZone.enabled = stat.isZoneAttack;


        uiDescription.text = stat.description;
        uiCout.text = stat.buyCost + "#";


        int level = stat.datas["Radius"].upgrateLevel;
        uiRadiusAdv.UpdateUi(level, "Radius");


        level = stat.datas["Damage"].upgrateLevel;
        uiDamageAdv.UpdateUi(level, "Damage");

        level = stat.datas["ReloadSpeed"].upgrateLevel;
        uiReloadSpeedAdv.UpdateUi(level, "ReloadSpeed");
    }

    void Update()
    {
        //UpdateUi();
    }
}
