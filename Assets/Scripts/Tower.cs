using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tower : MonoBehaviour
{


    Color color = Color.red;
    [BoxGroup("Target"), Label("Mob détectés")] public List<Collider> detectedEnemies;
    [BoxGroup("Target"), Label("Layer des mobs")] public LayerMask layerMask;
    [BoxGroup("Target")] [Label("Cible")] public List<GameObject> targets;
    public TowerStat statDefault;
    public TowerStat stat;
    [SerializeField] GameObject radius;
    [SerializeField] int radiusDefault = 25;
    public ParticleSystem shotPoint;

    //[BoxGroup("Systeme de tir"), ReadOnly, Required("Il est necessaire d'avoir un TowerBehaviour")] public TowerBehaviour slave;
    void Awake()
    {
        stat = Object.Instantiate(statDefault);
        stat.Init();
    }

    void Start()
    {
        UpdateInfo();
        DefineTarget();
    }

    void FixedUpdate()
    {
        //if (GetTarget() == null || GetTarget().Count == 0)
        DefineTarget();

        #region shot
        if (GetTarget() != null && GetTarget().Count > 0)
        {
            Shot();
        }
        #endregion


        ShotReload();
        UpdateInfo();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, stat.datas["Radius"].value);
        if (detectedEnemies != null)
        {
            if (detectedEnemies.Count != 0)
            {
                for (int i = detectedEnemies.Count - 1; i > 0; i--)
                {
                    Mind m = detectedEnemies[i].GetComponent<Mind>();
                    if (!m) continue;
                    if (m.IsDead()) continue;
                    Gizmos.DrawLine(transform.position, detectedEnemies[i].transform.position);
                }

                if (GetTarget().Count > 0)
                {
                    foreach (GameObject item in targets)
                    {
                        DebugExtension.DrawArrow(transform.position, item.transform.position - transform.position, Color.yellow);

                    }
                }
            }
        }
    }
    public List<GameObject> GetTarget()
    {

        List<int> detectedEnemiesToDelete = new List<int>();
        List<int> targetsToDelete = new List<int>();


        #region clear the list of detected ennemis
        if (detectedEnemies.Count > 0)
        {
            for (int i = detectedEnemies.Count - 1; i > 0; i--)
            {
                if (detectedEnemies[i] == null) { detectedEnemiesToDelete.Add(i); }

            }

            for (int i = 0; i < detectedEnemiesToDelete.Count; i++)
            {
                detectedEnemies.RemoveAt(detectedEnemiesToDelete[i]);
            }
        }
        #endregion

        #region clear the list of targets
        if (targets.Count > 0)
        {
            for (int i = targets.Count - 1; i > 0; i--)
            {
                Debug.Log(i);
                if (targets[i] == null)
                {
                    targetsToDelete.Add(i);
                }
                else
                {
                    Mind m = targets[i].GetComponent<Mind>();
                    if (!m || m.IsDead())
                    {
                        targetsToDelete.Add(i);
                    }
                }

            }

            for (int i = 0; i < targetsToDelete.Count; i++)
            {
                targets.RemoveAt(targetsToDelete[i]);
            }
        }
        #endregion





        if (targets.Count == 0 || detectedEnemies.Count == 0)
        {
            DefineTarget();
        }
        else
        {
            return targets;
        }
        return null;
    }
    public void DefineTarget()
    {


        targets.Clear();
        detectedEnemies.Clear();
        detectedEnemies = new List<Collider>(Physics.OverlapSphere(transform.position, stat.datas["Radius"].value, layerMask));

        if (stat.isZoneAttack)
        {
            if (detectedEnemies.Count > 0)
            {
                for (int i = detectedEnemies.Count - 1; i > 0; i--)
                {
                    if (detectedEnemies[i] == null) continue;
                    Mind mind = detectedEnemies[i].GetComponent<Mind>();
                    if (mind != null && !mind.IsDead())
                    {
                        targets.Add(detectedEnemies[i].gameObject);
                    }
                }
            }
        }
        else
        {
            targets.Add(detectedEnemies[0].gameObject);
        }
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


    bool Shotenabled = true;
    [BoxGroup("Stats"), ProgressBar("chargement")] public float shotLoading = 100;

    [BoxGroup("Stats"), Label("Vitesse de recharge")] public float shotStepLoad = 1;

    private void Shot()
    {
        if (Shotenabled)
        {
            Mind m;
            List<GameObject> obj = GetTarget();
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i] == null) continue;
                m = null;
                m = obj[i].GetComponent<Mind>();
                if (m == null) Debug.LogError("Cette ennemie n'a pas de Mind", obj[i]);

                m.Damage(stat.datas["Damage"].value);
                Shotenabled = false;
                shotLoading = 0;

                shotPoint.transform.LookAt(obj[i].transform);
                ShotParticle();


                if (m.IsDead())
                {
                    DefineTarget();
                }
            }
        }
    }

    [Button]
    private void ShotParticle()
    {
        if (!shotPoint.isPlaying)
            shotPoint.Play();
    }

    private void ShotReload()
    {
        if (shotLoading >= 100)
        {
            shotLoading = 100;
            Shotenabled = true;
        }
        else
        {
            shotLoading += shotStepLoad;
        }
    }
}
