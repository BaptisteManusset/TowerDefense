using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static public Main instance;

    [BoxGroup("Game Infos")] public bool isPaused = false;



    [BoxGroup("Destination tag")][Tag] public string destinationsTag;
    static public GameObject[] destinations;

    [BoxGroup("Stats")] public int money;
    [BoxGroup("Stats")] public int lifes = 20;


    [BoxGroup("Game Infos")] public GameObject camera;



    void Start()
    {
        if (Main.instance == null) Main.instance = this;
        Main.instance.camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void DecreaseLifes(int lifes = 1)
    {
        UpdateLifes(-lifes);
    }
    public void UpdateLifes(int lifes)
    {
        Main.instance.lifes += lifes;
        MainUI.instance.UpdateLifesUi();
    }

    public void UpdateMoney(int money)
    {
        Main.instance.money += money;
        MainUI.instance.UpdateMoneyUi();
    }
}
