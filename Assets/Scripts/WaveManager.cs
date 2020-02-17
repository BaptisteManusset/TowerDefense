using NaughtyAttributes;
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
            Debug.Log("Debug de la vague");

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
            Debug.Log("Fin de la vague");

            yield return new WaitForSeconds(delayBeetweenWave);
        }
    }

}
