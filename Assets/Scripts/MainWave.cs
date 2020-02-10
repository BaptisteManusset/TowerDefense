using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWave : MonoBehaviour
{
  public int waveCount = 5;
  public int count;
  public GameObject[] spawners;
  public GameObject[] mobs;
  public Transform parent;

  // Start is called before the first frame update
  void Start()
  {
    StartCoroutine(Waving());
  }

  IEnumerator Waving() // ~~¤_¤~~
  {
    for (int i = 0; i < count; i++)
    {
      for (int y = 0; y < spawners.Length; y++)
      {
        Instantiate(mobs[Random.Range(0, mobs.Length)], spawners[y].transform.position, Quaternion.identity, parent);
      }
      yield return new WaitForSeconds(.5f);
    }
    Debug.Log("Fin de wave");

    yield return new WaitForSeconds(10);

  }
}
