using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  //public Wave[] waves;


  //[Space(30)] public EnnemyToSpawn[] ennemyToSpawn;
  public int test;


  [System.Serializable]
  public class Wave
  {



    public string name;
    public float delayBeetweenWave;
    [BoxGroup("spawners")] [ReorderableList] public SpawnerElement[] spawners;

    public Wave()
    {
      name = "Vague";
    }

  }

  [System.Serializable]
  public class SpawnerElement
  {
    public string name = "SpawnerElement";
    public GameObject spawner;
    public EnnemyToSpawn[] ennemys;

  }


}

[System.Serializable]
public class EnnemyToSpawn
{
  [ShowAssetPreview] public GameObject ennemy;
  public int quantity;


}
