using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainLevel : MonoBehaviour
{
    static public MainLevel instance;




    [BoxGroup("Destination tag")] [Tag] public string destinationsTag;


    [BoxGroup("Game Infos")] [ReadOnly] public GameObject cam;
    public enum GameState { Wave, InterWave };
    [BoxGroup("Etat d'avancement")] public GameState gameState;

    [BoxGroup("Vie")] public FloatVariable health;
    [BoxGroup("Vie")] public FloatReference maxHealth;
    [BoxGroup("Argent")] public FloatVariable argent;
    [BoxGroup("Argent")] public float argentDefault = 1000;


    [BoxGroup("Game Infos")] [Label("Gameover")] public bool gameover;

    [BoxGroup("Events")] [SerializeField] float delayBeforeGameOver = 5;
    [BoxGroup("Events")] [SerializeField] UnityEvent GameOver;


    [SerializeField] UnityEvent StartEvent;

    void Awake()
    {
        health.SetValue(maxHealth);
        argent.SetValue(argentDefault);
        Time.timeScale = 1;
    }


    void Start()
    {
        if (MainLevel.instance == null) MainLevel.instance = this;
        cam = GameObject.FindGameObjectWithTag("MainCamera");

        StartEvent.Invoke();
    }

    [Button]
    public void Pause()
    {
        Time.timeScale = 0;
    }

    [Button]
    public void Unpause()
    {
        Time.timeScale = 1;
    }
    public void Accelerate(int value)
    {
        Time.timeScale = value;
    }

    void OnDisable()
    {

        Time.timeScale = 1;
    }

    public void LaunchGameOver()
    {
        gameover = true;
        GameOver.Invoke();
        Debug.Log("saluuuu");
    }
}
