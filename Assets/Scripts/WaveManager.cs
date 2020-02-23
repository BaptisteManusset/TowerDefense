using NaughtyAttributes;
using ScriptableVariable.Unite2017.Events;
using ScriptableVariable.Unite2017.Sets;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
  [BoxGroup("Delay")] public float delayBeetweenWave = 10;
  [BoxGroup("Delay")] public float delayBeetweenSpawn = 1;

  [BoxGroup("Waves")] [Label("List of Waves")] public Wave[] waves;


  [BoxGroup("Spawner")] public WaveSpawner[] waveSpawners;


  [BoxGroup("Parent")] public Transform parent;

  [BoxGroup("Events")] public GameEvent beginWave;
  [BoxGroup("Events")] public GameEvent endWave;
  [BoxGroup("Events")] public BoolVariable allWavesSpawnFinish;
  [BoxGroup("Variable")] public FloatVariable waveNumber;





  [System.Serializable]
  public class Wave
  {
    [ReadOnly] [SerializeField] string name = "Wave";
    [Header("-----------------------------------------------")] [Label("List of Mobs")] public mob[] mobs;
  }
  [System.Serializable]
  public class mob
  {

    [ReadOnly] [SerializeField] string name = "Quantity of mob to spawn";
    public GameObject prefab;
    [MinValue(1)] [MaxValue(20)] public int quantity = 5;
  }
  private void Start()
  {
    allWavesSpawnFinish.SetValue(false);

    StartCoroutine(WaveReader());
  }


  IEnumerator WaveReader()
  {
    for (int i = 0; i < waves.Length; i++)
    {
      beginWave.Raise();
      waveNumber.SetValue(i + 1);
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

    allWavesSpawnFinish.SetValue(true);
  }

}
