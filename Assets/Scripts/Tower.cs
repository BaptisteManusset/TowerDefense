using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tower : MonoBehaviour
{


    Color color = Color.red;
    [BoxGroup("Target"), Label("Mob détectés")] public List<Collider> hitColliders;
    [BoxGroup("Target"), Label("Layer des mobs")] public LayerMask layerMask;
    [BoxGroup("Target")] [Label("Cible")] public List<GameObject> target;
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

        UpdateInfo();
        DefineTarget();
    }

    void FixedUpdate()
    {
        //if (GetTarget() == null || GetTarget().GetComponent<Mind>().IsDead())
        if (GetTarget() == null || GetTarget().Count == 0)
            DefineTarget();

        #region shot
        if (GetTarget() != null && GetTarget().Count > 0)
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
            if (hitColliders.Count != 0)
            {
                for (int i = 0; i < hitColliders.Count; i++)
                {
                    if (!hitColliders[i].GetComponent<Mind>().IsDead())
                    {
                        Gizmos.DrawLine(transform.position, hitColliders[i].transform.position);
                    }
                }
                if (GetTarget().Count > 0)
                {
                    foreach (GameObject item in target)
                    {
                        DebugExtension.DrawArrow(transform.position, item.transform.position - transform.position, Color.yellow);

                    }
                }
            }
        }
    }
    public List<GameObject> GetTarget()
    {
        for (int i = hitColliders.Count - 1; i > 0; i--)
        {
            if (hitColliders[i] == null) hitColliders.RemoveAt(i);
        }


        for (int i = target.Count - 1; i > 0; i--)
        {
            if (target[i] == null || target[i].GetComponent<Mind>().IsDead()) target.RemoveAt(i);
        }


        if (target.Count == 0 || hitColliders.Count == 0)
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

        target.Clear();
        hitColliders = new List<Collider>(Physics.OverlapSphere(transform.position, stat.datas["Radius"].value, layerMask));

        //if (hitColliders.Length > 0)
        //{
        if (stat.isZoneAttack)
        {
            for (int i = 0; i < hitColliders.Count; i++)
            {
                if (!hitColliders[i].GetComponent<Mind>().IsDead())
                {
                    target.Add(hitColliders[i].gameObject);
                }
            }
        }
        else
        {
            target.Add(hitColliders[0].gameObject);
        }
        //}
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
    public void UpdateInfo()
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
