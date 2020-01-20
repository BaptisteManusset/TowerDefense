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
    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiDescription;
    [BoxGroup("UI")] [SerializeField] TextMeshProUGUI uiCout;

    void Awake()
    {
        UpdateUi();
    }
    public void BuyTower()
    {
        //if (argent.Value >= 0)
        //{
        tour.Value = tourToSell;
        //}
    }

    public void UpdateUi(GameObject obj)
    {
        tourToSell = obj;
        UpdateUi();
    }


    [Button]
    public void UpdateUi()
    {
        if (tour.Value == null)
        {
            Debug.LogError("Aucune tour selectionné", tour);
            return;
        }

        GameObject t = tour.Value.gameObject;
        Debug.Log($"{t.name} {t.GetComponent<Tower>()} {t.GetComponent<Tower>().statDefault}", t);

        TowerStat stat = t.GetComponent<Tower>().statDefault;
        uiName.text = tourToSell.name;
        uiRadius.text = stat.datas["Radius"].upgrateLevel.ToString();
        uiPower.text = stat.datas["Damage"].upgrateLevel.ToString();
        uiReloadPower.text = stat.datas["ReloadSpeed"].upgrateLevel.ToString();
        uiDescription.text = stat.description;
        uiCout.text = stat.buyCost + "#";
    }
}
