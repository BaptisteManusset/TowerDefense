using NaughtyAttributes;
using ScriptableVariable.Unite2017.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainMode : MonoBehaviour
{
    private MainLevel master;
    [SerializeField] List<WaveData> waves = new List<WaveData>();
    public WaveSpawner[] spawners;
    int spwnCount = 0;
    int spwnReadyCount = 0;
    public UnityEvent waveBegin;
    public UnityEvent waveEnd;
    int waveSelect = 0;



    void Awake()
    {
        master = GetComponent<MainLevel>();
    }


    void Start()
    {
        spawners = FindObjectsOfType<WaveSpawner>();
        waveBegin.Invoke();
    }


    void Update()
    {
        if (spwnCount == spwnReadyCount)
        {
            waveEnd.Invoke();

            spwnReadyCount = 0;
            waveSelect++;

            waveBegin.Invoke();
        }
    }

    public void Increase()
    {
        spwnReadyCount++;
    }


    public WaveData GetWave()
    {
        return waves[waveSelect];
    }
}
