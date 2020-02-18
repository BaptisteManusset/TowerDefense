using NaughtyAttributes;
using ScriptableVariable.Unite2017.Events;
using ScriptableVariable.Unite2017.Sets;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  public float delayBeetweenWave = 10;
  public float delayBeetweenSpawn = 1;

  public Wave[] waves;
  public WaveSpawner[] waveSpawners;
  public Transform parent;

  [BoxGroup("Events")] public GameEvent beginWave;
  [BoxGroup("Events")] public GameEvent endWave;



  [System.Serializable]
  public class Wave
  {
    [ReorderableList] public mob[] mobs;
  }
  [System.Serializable]
  public class mob
  {
    public GameObject prefab;
    public int quantity = 5;
  }
  private void Start()
  {


    StartCoroutine(WaveReader());
  }


  IEnumerator WaveReader()
  {
    for (int i = 0; i < waves.Length; i++)
    {
      Debug.Log("<color=red>Debug de la vague</color>");
      beginWave.Raise();
      #region wave spawner
      for (int m = 0; m < waves[i].mobs.Length; m++)
      {
        for (int p = 0; p < waves[i].mobs[m].quantity; p++)
        {
          for (int s = 0; s < waveSpawners.Length; s++)
          {
            waveSpawners[s].SpawnMob(waves[i].mobs[m].prefab, parent);

          }
          yield return new WaitForSeconds(delayBeetweenSpawn);
        }
      }
      #endregion
      endWave.Raise();

      Debug.Log("<color=red>Fin de la vague</color>");

      if (delayBeetweenWave > 5)
      {

        for (int d = 0; d < 5; d++)
        {

          yield return new WaitForSeconds(1f);
          Debug.Log("<color=red>[" + (5 - d) + "] Temps restant avant la prochaine vague </color>");

        }

        yield return new WaitForSeconds(delayBeetweenWave - 5);
      } else
      {
        yield return new WaitForSeconds(delayBeetweenWave);
      }
    }
  }

}
