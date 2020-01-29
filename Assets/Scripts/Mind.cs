using NaughtyAttributes;
using ScriptableVariable.Unite2017.Sets;
using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Mind : MonoBehaviour
{
  #region stats    
  [BoxGroup("Stats")] [ProgressBar(null, 10)] public int health;
  [BoxGroup("Stats")] public Image healthbar;
  int maxHealth = 10;
  bool alive = true;
  #endregion

  NavMeshAgent meshAgent;
  Canvas canvas;
  Renderer cubeRenderer;

  [BoxGroup("Deplacement")] public Transform destination;
  [BoxGroup("Deplacement")] [Tag]public string tagDeplacement;
  //[BoxGroup("Valeur à la mort")] public int valeur = 10;

  public FloatReference damageToTower;
  [BoxGroup("Argent")] public FloatVariable argent;
  [BoxGroup("Argent")] [Label("Valeur")] public FloatReference valeur;
  [SerializeField] ThingRuntimeSet destinations;
  public float speed = 3.5f;
  public float radius = .4f;



  void Start()
  {
    health = maxHealth;
    #region definition de la destination
    meshAgent = GetComponent<NavMeshAgent>();



    meshAgent.speed = speed;
    meshAgent.radius = radius;

    //if (destinations == null)
    //{
    //    destinations = GameObject.FindGameObjectsWithTag("Destination");
    //}
    //destination = destinations.Items[Random.Range(0, destinations.Items.Count)].transform;
    destination = GameObject.FindGameObjectWithTag(tagDeplacement).transform;
    if (destination == null) Debug.LogError("Impossible de trouver une destination");
    meshAgent.SetDestination(destination.position);
    #endregion
    cubeRenderer = GetComponent<Renderer>();
    canvas = GetComponentInChildren<Canvas>();
    UpdateHealthBar();



  }
  void Update()
  {
    canvas.transform.LookAt(MainLevel.instance.cam.transform);
    NavMeshHit hit;
    meshAgent.SamplePathPosition(-1, 0.0f, out hit);
  }
  public void Damage(int d)
  {
    if (alive)
    {
      health -= d;
    }
    if (health <= 0)
    {
      Dead();
    }
    UpdateHealthBar();
  }

  void Dead()
  {
    if (alive)
    {
      health = 0;
      alive = false;
      meshAgent.isStopped = true;
      cubeRenderer.material.SetColor("_BaseColor", Color.red);


      argent.ApplyChange(valeur);
      Destroy(gameObject, 2);
    }
  }
  public bool IsDead()
  {
    return !alive;
  }

  private void UpdateHealthBar()
  {
    healthbar.fillAmount = (float)health / (float)maxHealth;
  }
}
