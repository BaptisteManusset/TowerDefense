using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using ScriptableVariable.Unite2017.Sets;
using ScriptableVariable.Unite2017.Events;

public class MainLevel : MonoBehaviour
{
  static public MainLevel instance;




  [BoxGroup("Destination tag")] [NaughtyAttributes.Tag] public string destinationsTag;


  [BoxGroup("Game Infos")] [ReadOnly] public GameObject cam;
  //public enum GameState { Wave, InterWave };
  //[BoxGroup("Etat d'avancement")] public GameState gameState;

  [BoxGroup("Vie")] public FloatVariable health;
  [BoxGroup("Vie")] public FloatReference maxHealth;
  [BoxGroup("Argent")] public FloatVariable argent;
  [BoxGroup("Argent")] public float argentDefault = 1000;

  [BoxGroup("Score")] public FloatVariable score;

  [BoxGroup("Game Infos")] [Label("Gameover")] public bool gameover;

  [BoxGroup("Events")] [SerializeField] float delayBeforeGameOver = 5;
  [BoxGroup("Events")] [SerializeField] UnityEvent GameOver;
  int mm = 0;
  int s = 0;
  int m = 0;


  [SerializeField] UnityEvent StartEvent;


  [BoxGroup("Timer")] [SerializeField] FloatVariable timer;
  [BoxGroup("Timer")] [SerializeField] TextMeshProUGUI timerText;

  public BoolVariable allSpawnFinished;
  public ThingRuntimeSet ennemy;
  public GameEvent winEvent;


  void Awake()
  {
    if (MainLevel.instance == null) MainLevel.instance = this;


    health.SetValue(maxHealth);
    argent.SetValue(argentDefault);
    score.SetValue(0);
    timer.SetValue(0);

    Time.timeScale = 1;
  }


  void Start()
  {
    cam = GameObject.FindGameObjectWithTag("MainCamera");
    StartEvent.Invoke();

  }

  private void Update()
  {
    if (!gameover)
    {
      timer.Value += Time.deltaTime;
      mm = (int)(timer.Value * 100);
      mm %= 100;
      s = Mathf.RoundToInt(timer.Value);
      m = s / 60;
      timerText.text = $"{m}:{s}.<size=75%>{mm}</size>";
    }


    if (allSpawnFinished.Value)
    {
      if (ennemy.Items.Count <= 0)
      {
        winEvent.Raise();
      }
    }
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

  }
}
