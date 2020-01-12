using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  private MainLevel m;
  public GameObject prefab;
  public Transform parent;
  public int quantite = 5;
  public float delay = .1f;
  [SerializeField] [ReadOnly] bool spawnIsLaunch = false;

  void Awake()
  {
    m = FindObjectOfType<MainLevel>();
  }

  void Update()
  {
    if (m.gameState == MainLevel.GameState.Wave && spawnIsLaunch == false)
    {

      spawnIsLaunch = true;
      StartCoroutine(SpawnLoop());

    } else if (m.gameState == MainLevel.GameState.InterWave && spawnIsLaunch == true)
    {
      spawnIsLaunch = false;
    }


  }
  IEnumerator SpawnLoop()
  {
    for (int i = 0; i < quantite; i++)
    {
      Instantiate(prefab, transform.position, transform.rotation, parent);
      yield return new WaitForSeconds(delay);
    }

  }



}
