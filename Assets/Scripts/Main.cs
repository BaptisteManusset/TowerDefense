using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
  static public Main instance;

  [BoxGroup("Game Infos")][Label("Pause")] public bool isPaused = false;
  [BoxGroup("Game Infos")] [Label("Gameover")] public bool gameover;



  [BoxGroup("Destination tag")] [Tag] public string destinationsTag;
  static public GameObject[] destinations;

  [BoxGroup("Stats")] public int money;
  [BoxGroup("Stats")] public int lifes = 20;


  [BoxGroup("Game Infos")] [ReadOnly] public GameObject camera;
  MainUI mainui;
  


  void Awake()
  {
    mainui = GetComponent<MainUI>();
  }


  void Start()
  {
    if (Main.instance == null) Main.instance = this;
    camera = GameObject.FindGameObjectWithTag("MainCamera");
  }

  public void DecreaseLifes(int lifes = 1)
  {
    UpdateLifes(-lifes);
  }
  public void UpdateLifes(int value)
  {
    lifes += value;
    if(lifes <= 0)
    {
      lifes = 0;
      gameover = true;
    }
    mainui.UpdateLifesUi();
  }

  public void UpdateMoney(int value)
  {

    money += value;
    mainui.UpdateMoneyUi();
  }
}
