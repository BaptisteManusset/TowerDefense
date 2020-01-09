using NaughtyAttributes;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [BoxGroup("Gizmos")] public float detectionRadius;
    Color color = Color.red;
    [BoxGroup("Target")] [Label("Mob détectés")] public Collider[] hitColliders;
    [BoxGroup("Target")] [Label("Layer des mobs")] public LayerMask layerMask;
    [BoxGroup("Target")] [Label("Cible")] [ReadOnly] public int target = -1;

    bool Shotenabled = true;
    [BoxGroup("Stats")] [Label("Vitesse de recharge")] public float shotStepLoad = 1;
    [BoxGroup("Stats")]
    [ProgressBar("chargement")]
    public float shotLoading = 100;
    [BoxGroup("Stats")] public int damage = 10;



    void Update()
    {
        DefineTarget();

        #region shot
        if (GetTarget())
        {
            Shot();
        }
        #endregion


        ShotReload();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        if (hitColliders != null)
        {
            if (hitColliders.Length != 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (!hitColliders[i].GetComponent<Mind>().IsDead())
                        Gizmos.DrawLine(transform.position, hitColliders[i].transform.position);
                }
            }
        }

        if (target != -1)
            DebugExtension.DrawArrow(transform.position, GetTarget().transform.position - transform.position, Color.yellow);
    }


    GameObject GetTarget()
    {
        try
        {
            return hitColliders[target].gameObject;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
    void DefineTarget()
    {
        hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, layerMask);
        if (target == -1)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].gameObject.GetComponent<Mind>().IsDead() == false)
                    target = i;
            }
        }

    }
    void Shot()
    {
        if (Shotenabled)
        {
            Mind m = GetTarget().GetComponent<Mind>();
            m.Damage(damage);
            Shotenabled = false;
            shotLoading = 0;
            if (m.IsDead()) target = -1;
        }

    }

    void ShotReload()
    {

        if (shotLoading >= 100)
        {
            shotLoading = 100;
            Shotenabled = true;
        }
        else
        {
            shotLoading += shotStepLoad * Time.deltaTime * 100;
        }
    }
}
