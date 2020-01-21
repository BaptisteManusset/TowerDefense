using NaughtyAttributes;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    public bool onWave = false;
    public WaveData wave;
    public float delay;
    public Transform parent;
    public MainMode mainMode;

    

    [Button]
    public void BeginWave()
    {
        wave = mainMode.GetWave();
        Debug.Log("BeginWave");
        onWave = true;
        StartCoroutine(Spawing());
        onWave = false;
    }

    [Button]
    IEnumerator Spawing()
    {
        Debug.Log("couroutine");
        for (int i = 0; i < wave.quantite; i++)
        {
            int t = Random.Range(0, wave.spawner.Length);
            Instantiate(wave.spawner[t], transform.position, Quaternion.identity, parent);
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(10);
        mainMode.Increase();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3);
    }
}
