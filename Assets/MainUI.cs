using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    [BoxGroup("Argent")] public TextMeshProUGUI TxtMoney;
    [BoxGroup("Vies")] public TextMeshProUGUI TxtLife;

    static public MainUI instance;
    void Start()
    {
        if (Main.instance == null) MainUI.instance = this;

        


    }


    public void UpdateAllUi()
    {

        MainUI.instance.UpdateLifesUi();
        MainUI.instance.UpdateMoneyUi();
    }


    public void UpdateLifesUi()
    {
        TxtLife.text = (Main.instance.lifes).ToString() + " Vie";
    }

    public void UpdateMoneyUi()
    {
        TxtMoney.text = (Main.instance.money).ToString();
    }
}
