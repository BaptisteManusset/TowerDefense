using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Mind : MonoBehaviour
{
    #region stats    
    [BoxGroup("Stats")] [ProgressBar(null, 10)] public int health;
    [BoxGroup("Stats")] public Image healthbar;
    int maxHealth = 10;
    bool alive = true;
    #endregion

    NavMeshAgent MeshAgent;
    Canvas canvas;
    Renderer cubeRenderer;

    [BoxGroup("Deplacement")] public Transform destination;
    [BoxGroup("Valeur à la mort")] public int valeur = 10;

    void Start()
    {
        health = maxHealth;
        #region definition de la destination
        MeshAgent = GetComponent<NavMeshAgent>();
        if (Main.destinations == null)
        {
            Main.destinations = GameObject.FindGameObjectsWithTag("Destination");
        }
        destination = Main.destinations[Random.Range(0, Main.destinations.Length)].transform;
        if (destination == null) Debug.LogError("Impossible de trouver une destination");
        MeshAgent.SetDestination(destination.position);
        #endregion
        cubeRenderer = GetComponent<Renderer>();
        canvas = GetComponentInChildren<Canvas>();
        UpdateHealthBar();



    }

    void Update()
    {
        canvas.transform.LookAt(Main.instance.camera.transform);



        NavMeshHit hit;
        MeshAgent.SamplePathPosition(-1, 0.0f, out hit);
        Debug.Log($"hit.mask >> {hit.mask}");


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
            MeshAgent.isStopped = true;

            cubeRenderer.material.SetColor("_BaseColor", Color.red);


            Main.instance.UpdateMoney(valeur);
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
