using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    static public Main instance;

    public bool isPaused = false;



    [BoxGroup("Destination tag")] public string destinationsTag;
    static public GameObject[] destinations;

    public int money;
    public int lifes = 20;


    public GameObject camera;



    void Start()
    {
        if (Main.instance == null) Main.instance = this;
        Debug.Log(Main.instance);

        Main.instance.camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {

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
