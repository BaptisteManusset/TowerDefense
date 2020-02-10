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
  // string set in readonly for prevent edit and fuck up the mind of my ennemy
  [BoxGroup("Deplacement")] [SerializeField] [ReadOnly] string tagDestination = "Destination";
  //[BoxGroup("Valeur à la mort")] public int valeur = 10;

  public FloatReference damageToTower;
  [BoxGroup("Argent")] public FloatVariable argent;
  [BoxGroup("Argent")] [Label("Valeur")] public FloatReference valeur;
  [SerializeField] ThingRuntimeSet destinations;
  public float speed = 3.5f;
  //public float radius = .4f;
  public float angularSpeed = 20;
  public float acceleration = 10;



  void Start()
  {
    health = maxHealth;
    #region definition de la destination
    meshAgent = GetComponent<NavMeshAgent>();



    meshAgent.speed += Random.Range(-speed, speed);
    //meshAgent.radius += Random.Range(-radius, radius);
    meshAgent.angularSpeed  += Random.Range(-angularSpeed, angularSpeed);
    meshAgent.acceleration += Random.Range(-acceleration, acceleration);
    

    destination = GameObject.FindGameObjectWithTag(tagDestination).transform;
    if (destination == null) Debug.LogError("Impossible de trouver une destination");
    meshAgent.SetDestination(destination.position);
    #endregion
    cubeRenderer = GetComponent<Renderer>();
    canvas = GetComponentInChildren<Canvas>();
    UpdateHealthBar();



  }
  void Update()
  {
    NavMeshHit hit;
    meshAgent.SamplePathPosition(-1, 0.0f, out hit);
  }

    private void LateUpdate()
    {
        canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.forward);
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
      GetComponent<Collider>().enabled = false;

      health = 0;
      alive = false;
      meshAgent.isStopped = true;
      cubeRenderer.material.SetColor("_BaseColor", Color.red);


      argent.ApplyChange(valeur);
      //Destroy(gameObject, 2);
      Destroy(gameObject);


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
