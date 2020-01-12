using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMode : MonoBehaviour
{
  private MainLevel master;
  //private MainUI ui;

  void Awake()
  {
    master = GetComponent<MainLevel>();
    //ui = GetComponent<MainUI>();
  }

  [Button("Lancer la vague")]
  public void SetModeWave()
  {
    master.gameState = MainLevel.GameState.Wave;
  }
  [Button("Inter-vague")]
  public void SetModeInterWave()
  {
    master.gameState = MainLevel.GameState.InterWave;
  }
}
