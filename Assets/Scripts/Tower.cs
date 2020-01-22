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
    [SerializeField] GameObject radius;
    [SerializeField] int radiusDefault = 25;





    [BoxGroup("Systeme de tir"), ReadOnly, Required("Il est necessaire d'avoir un TowerBehaviour")] public TowerBehaviour slave;
    void Awake()
    {
        stat = Object.Instantiate(statDefault);
        stat.Init();

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
        UpdateInfo();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        if (slave)
            Gizmos.DrawWireSphere(transform.position, stat.datas["Radius"].value);
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
        hitColliders = Physics.OverlapSphere(transform.position, stat.datas["Radius"].value, layerMask);
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


    public void sellTower()
    {
        Destroy(gameObject);
    }

    //update radius of detection
    private void UpdateInfo()
    {
        int scale = stat.datas["Radius"].upgrateLevel * 8 + radiusDefault;

        radius.transform.localScale = Vector3.one * scale;
        scale /= 2; // diameter to radius
        stat.datas["Radius"].value = scale;
    }


    public void OnEnter()
    {
        Debug.Log("OnEnter");
    }
    public void OnQuit()
    {
        Debug.Log("OnQuit");
    }

}
