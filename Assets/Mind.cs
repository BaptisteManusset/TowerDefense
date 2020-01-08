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


    void Start()
    {
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
    }
    public bool IsDead()
    {
        return !alive;
    }
}
