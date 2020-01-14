using NaughtyAttributes;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tower : MonoBehaviour
{


    Color color = Color.red;
    [BoxGroup("Target"), Label("Mob détectés")] public Collider[] hitColliders;
    [BoxGroup("Target"), Label("Layer des mobs")] public LayerMask layerMask;
    [BoxGroup("Target")] [Label("Cible")] [ShowAssetPreview] public GameObject target;
    public TowerStat statDefault;
    public TowerStat stat;




    [BoxGroup("Systeme de tir"), ReadOnly, Required("Il est necessaire d'avoir un TowerBehaviour")] public TowerBehaviour slave;
    void Awake()
    {
        stat = Object.Instantiate(statDefault);

        UpdateTowerBehaviour();
    }

    void Start()
    {
        DefineTarget();
    }

    void FixedUpdate()
    {
        if (GetTarget() == null || GetTarget().GetComponent<Mind>().IsDead())
            DefineTarget();

        #region shot
        if (GetTarget())
        {
            slave.Shot();
        }
        #endregion


        slave.ShotReload();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        if (slave)
            Gizmos.DrawWireSphere(transform.position, stat.reloadSpeed);
        if (hitColliders != null)
        {
            if (hitColliders.Length != 0)
            {
                for (int i = 0; i < hitColliders.Length; i++)
                {
                    if (!hitColliders[i].GetComponent<Mind>().IsDead())
                    {
                        Gizmos.DrawLine(transform.position, hitColliders[i].transform.position);
                    }
                }
                if (GetTarget())
                    DebugExtension.DrawArrow(transform.position, GetTarget().transform.position - transform.position, Color.yellow);
            }
        }
    }
    public GameObject GetTarget()
    {
        if (target == null)
        {
            DefineTarget();
        }
        else
        {
            return target;
        }
        return null;
    }
    public void DefineTarget()
    {
        hitColliders = Physics.OverlapSphere(transform.position, stat.reloadSpeed, layerMask);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (!hitColliders[i].GetComponent<Mind>().IsDead())
            {
                target = hitColliders[i].gameObject;
            }
        }
    }
    [Button("Definir un TowerBehaviour")]
    void UpdateTowerBehaviour()
    {
        slave = GetComponent<TowerBehaviour>();
        if (slave != null)
        {
            slave.master = this;
        }
        else
        {
            Debug.LogError("Argh! Il manque un TowerBehaviour ici!");
        }
    }
    [Button("Reset le TowerBehaviour")]
    void ResetTowerBehaviour()
    {
        DestroyImmediate(slave);
        slave = null;
    }
}
