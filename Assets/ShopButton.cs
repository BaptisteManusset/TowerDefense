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


    //[BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiRadius;


    [BoxGroup("UI")] [SerializeField] uiStatDisplayDot uiRadiusAdv;
    [BoxGroup("UI")] [SerializeField] uiStatDisplayDot uiDamageAdv;
    [BoxGroup("UI")] [SerializeField] uiStatDisplayDot uiReloadSpeedAdv;


    //[BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiPower;


    //[BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiReloadPower;


    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiDescription;


    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiCout;

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


        //if (tour.Value == null)
        //{
        //    Debug.LogError("Aucune tour selectionné", tour);
        //    return;
        //}

        //GameObject t = tour.Value.gameObject;
        
        //TowerStat stat = t.GetComponent<Tower>().statDefault;
        TowerStat stat = tourToSell.GetComponent<Tower>().statDefault;
        uiName.text = tourToSell.name;


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
