#pragma warning disable 0649
using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tower : MonoBehaviour
{


  Color color = Color.red;
  [BoxGroup("Target"), Label("Mob détectés")] public List<Mind> detectedEnemies;
  //[BoxGroup("Target")] [Label("Cible")] public List<Mind> targets;

  [BoxGroup("Target"), Label("Layer des mobs")] public LayerMask layerMask;


  // Must be public
  [BoxGroup("Stats")] public TowerStat statDefault;
  [BoxGroup("Stats")] public TowerStat stat;


  [SerializeField] GameObject radius;
  [SerializeField] int radiusDefault = 25;

  [BoxGroup("Reload")] [SerializeField] [Label("Tir prés")] bool reloadRequire = false;
  [BoxGroup("Reload")] [ProgressBar("chargement")] public float shotLoading = 100;

  [BoxGroup("FX")] [SerializeField] ParticleSystem shotPoint;
  [BoxGroup("FX")] [SerializeField] ParticleSystem muzzle;


  [BoxGroup("Externe")] [SerializeField] BoolVariable showRadius;


  private LineRenderer lineRenderer;

  void Awake()
  {
    stat = Object.Instantiate(statDefault);
    stat.Init();
    lineRenderer = GetComponent<LineRenderer>();
    lineRenderer.positionCount = 0;

    radius.SetActive(showRadius.Value);
  }
  void Start()
  {
    UpdateInfo();
  }
  void Update()
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


    if (reloadRequire == true)
    {
      lineRenderer.positionCount = 0;
      shotLoading += stat.datas["ReloadSpeed"].upgrateLevel;

      if (shotLoading >= 100)
      {
        reloadRequire = false;
        shotLoading = 0;
      }
    }


    UpdateInfo();
  }
  void OnDrawGizmos()
  {

    Gizmos.color = color;
    if (stat)
      Gizmos.DrawWireSphere(transform.position, stat.datas["Radius"].value);
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
        foreach (Mind item in detectedEnemies)
        {
          DebugExtension.DrawArrow(transform.position, item.transform.position - transform.position, Color.yellow);

        }
      }

    } else
    {
      muzzle.Stop();
    }
  }
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
    lineRenderer.positionCount = 2;
    if (reloadRequire == false)
    {
      reloadRequire = true;
      foreach (Mind item in detectedEnemies)
      {
        if (item.IsDead()) continue;
        item.Damage(stat.datas["Damage"].value * stat.datas["Damage"].upgrateLevel);
        #region juicy
        lineRenderer.SetPosition(0, shotPoint.transform.position);
        lineRenderer.SetPosition(1, item.gameObject.transform.position);
        shotPoint.transform.LookAt(item.transform);
        shotPoint.Play();
        muzzle.Play();
        #endregion
      }
      //Invoke("Reload", 5 / stat.datas["ReloadSpeed"].upgrateLevel);
    }
  }

  public void RadiusDisplay()
  {
    radius.SetActive(true);
  }
  public void RadiusHide()
  {
    radius.SetActive(showRadius.Value);
  }
}
