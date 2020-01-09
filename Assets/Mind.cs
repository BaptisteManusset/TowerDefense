using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
public class Mind : MonoBehaviour
{
    #region stats    
    [BoxGroup("Stats")]
    [ProgressBar(null, 10)]
    public int health = 10;
    int maxHealth = 10;
    bool alive = true;
    #endregion


    NavMeshAgent MeshAgent;
    [BoxGroup("Deplacement")] public Transform destination;
    Renderer cubeRenderer;

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();


        MeshAgent = GetComponent<NavMeshAgent>();
        MeshAgent.SetDestination(destination.position);
    }

    public void Damage(int d)
    {

        if (alive)
        {
            health -= d;
        }
        if (health <= 0)
        {
            dead();
        }
    }

    void dead()
    {
        alive = false;
        MeshAgent.isStopped = true;

        cubeRenderer.material.SetColor("_BaseColor", Color.red);

        //Call SetColor using the shader property name "_Color" and setting the color to red
    }
    public bool IsDead()
    {
        return !alive;
    }
}
