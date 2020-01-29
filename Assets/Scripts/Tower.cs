#pragma warning disable 0649
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tower : MonoBehaviour
{


  Color color = Color.red;
  [BoxGroup("Target"), Label("Mob détectés")] public List<Mind> detectedEnemies;
  [BoxGroup("Target")] [Label("Cible")] public List<Mind> targets;

  [BoxGroup("Target"), Label("Layer des mobs")] public LayerMask layerMask;
  public TowerStat statDefault;
  public TowerStat stat;
  [SerializeField] GameObject radius;
  [SerializeField] int radiusDefault = 25;
  public ParticleSystem shotPoint;



  [SerializeField] bool reloadRequire = false;
  [BoxGroup("Stats"), ProgressBar("chargement")] public float shotLoading = 100;
  [BoxGroup("Stats"), Label("Vitesse de recharge")] public float shotStepLoad = 1;


  //[BoxGroup("Systeme de tir"), ReadOnly, Required("Il est necessaire d'avoir un TowerBehaviour")] public TowerBehaviour slave;
  void Awake()
  {
    stat = Object.Instantiate(statDefault);
    stat.Init();
  }
  void Start()
  {
    UpdateInfo();
    //DefineTarget();
  }
  void FixedUpdate()
  {
    detectedEnemies.Clear();
    Collider[] tempArray = Physics.OverlapSphere(transform.position, stat.datas["Radius"].value, layerMask);
    foreach (Collider item in tempArray)
    {
      Mind mind = item.GetComponent<Mind>();
      if (mind)
      {
        if (mind.IsDead() == false)
        {
          detectedEnemies.Add(mind);

          if (stat.isZoneAttack == false)
          {
            break;
          }
        }
      }
    }




    if (detectedEnemies.Count > 0)
      Shot();


    UpdateInfo();
  }
  void OnDrawGizmos()
  {

    Gizmos.color = color;
    Gizmos.DrawWireSphere(
      transform.position,
      stat.datas["Radius"].value);
    //if (detectedEnemies != null)
    //{
    if (detectedEnemies.Count != 0)
    {
      for (int i = 0; i < detectedEnemies.Count; i++)
      {
        Mind m = detectedEnemies[i].GetComponent<Mind>();
        if (!m) continue;
        if (m.IsDead()) continue;
        Gizmos.DrawLine(transform.position, detectedEnemies[i].transform.position);
      }

      if (detectedEnemies.Count > 0)
      {
        foreach (Mind item in targets)
        {
          DebugExtension.DrawArrow(transform.position, item.transform.position - transform.position, Color.yellow);

        }
      }

    }
  }
  //public List<Mind> GetTarget()
  //{
  //  if (detectedEnemies.Count == 0)
  //  {
  //    DefineTarget();
  //  } else
  //  {
  //    return targets;
  //  }
  //  return null;
  //}
  //public void DefineTarget()
  //{
  //  detectedEnemies.Clear();
  //  var colliders = new List<Collider>(Physics.OverlapSphere(transform.position, stat.datas["Radius"].value, layerMask));

  //  for (int i = 0; i < colliders.Count; i++)
  //  {
  //    Mind m = colliders[i].GetComponent<Mind>();
  //    if (!detectedEnemies.Contains(m))
  //      detectedEnemies.Add(m);
  //  }

  //  for (int i = detectedEnemies.Count; i != 0; i--)
  //  {
  //    if (detectedEnemies[i] == null)
  //    {
  //      detectedEnemies.RemoveAt(i);
  //    } else
  //    {
  //      if (detectedEnemies[i].IsDead()) detectedEnemies.RemoveAt(i);
  //    }
  //  }

  //}

  public void sellTower()
  {
    Destroy(gameObject);
  }
  public void UpdateInfo()
  {
    int scale = stat.datas["Radius"].upgrateLevel * 8 + radiusDefault;

    radius.transform.localScale = Vector3.one * scale;
    scale /= 2; // diameter to radius
    stat.datas["Radius"].value = scale;
  }


  void Shot()
  {
    if (reloadRequire == false)
    {
      reloadRequire = true;
      foreach (Mind item in detectedEnemies)
      {
        item.Damage(stat.datas["Damage"].value);

        shotPoint.transform.LookAt(item.transform);
        shotPoint.Play();

      }
      Invoke("Reload", 1);

    }
  }

  void Reload()
  {
    reloadRequire = false;
  }

}
