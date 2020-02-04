#pragma warning disable 0649
using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tower : MonoBehaviour
{


  Color color = Color.red;
  [BoxGroup("Target"), Label("Mob détectés")] public List<Mind> detectedEnemies;

  [BoxGroup("Target"), Label("Layer des mobs")] public LayerMask layerMask;


  // Must be public
  [BoxGroup("Stats")] public TowerStat statDefault;
  [BoxGroup("Stats")] public TowerStat stat;


  [SerializeField] Renderer radius;
  [SerializeField] int radiusDefault = 25;

  [BoxGroup("Reload")] [SerializeField] [Label("Tir prés")] bool reloadRequire = false;
  [BoxGroup("Reload")] [ProgressBar("chargement")] public float shotLoading = 100;

  [BoxGroup("FX")] [SerializeField] ParticleSystem shotPoint;
  [BoxGroup("FX")] [SerializeField] ParticleSystem muzzle;
  [BoxGroup("FX")] [SerializeField] ParticleSystem loading;


  [BoxGroup("Externe")] [SerializeField] BoolVariable showRadius;


  private LineRenderer lineRenderer;

  void Awake()
  {
    stat = Object.Instantiate(statDefault);
    stat.Init();
    lineRenderer = GetComponent<LineRenderer>();
    //lineRenderer.positionCount = 0;

    //radius.SetActive(showRadius.Value);
    radius.material.SetFloat("_Display", showRadius.Value ? 1 : 0);
  }
  void Start()
  {
    UpdateInfo();
  }
  void FixedUpdate()
  {
    detectedEnemies.Clear();
    Collider[] tempArray = Physics.OverlapSphere(transform.position, stat.datas["Radius"].value, layerMask);

    #region get all ennemys in range
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
    #endregion




    if (detectedEnemies.Count > 0)
    {
      StartCoroutine(Shot());
    } else
    {
      AnimStop();
    }



    #region reloading  
    if (reloadRequire == true)
    {
      loading.Play();
      shotLoading += stat.datas["ReloadSpeed"].upgrateLevel;

      if (shotLoading >= 100)
      {
        reloadRequire = false;
        shotLoading = 0;
        loading.Stop();
      }
    }
    #endregion

    UpdateInfo();
  }
  void OnDrawGizmos()
  {

    Gizmos.color = color;
    if (stat)
      Gizmos.DrawWireSphere(transform.position, stat.datas["Radius"].value);
    if (detectedEnemies.Count >= 0)
    {
      for (int i = 0; i < detectedEnemies.Count; i++)
      {
        Mind m = detectedEnemies[i].GetComponent<Mind>();
        if (!m) continue;
        if (m.IsDead()) continue;


        Gizmos.DrawLine(transform.position, detectedEnemies[i].transform.position);
        DebugExtension.DrawArrow(transform.position, detectedEnemies[i].transform.position - transform.position, Color.yellow);

      }
    }
  }

  public void UpdateInfo()
  {
    int scale = stat.datas["Radius"].upgrateLevel * 8 + radiusDefault;

    radius.transform.localScale = Vector3.one * scale;
    scale /= 2; // diameter to radius
    stat.datas["Radius"].value = scale;
  }

  IEnumerator Shot()
  {
    if (reloadRequire == false)
    {
      if (!shotPoint.isPlaying)
        shotPoint.Play();

      if (!muzzle.isPlaying)
        muzzle.Play();

      foreach (Mind item in detectedEnemies)
      {
        if (item.IsDead()) continue;
        AnimPlay(item.gameObject);
      }

      yield return new WaitForSeconds(1f);


      reloadRequire = true;


      foreach (Mind item in detectedEnemies)
      {
        if (item.IsDead()) continue;
        item.Damage(stat.datas["Damage"].value * stat.datas["Damage"].upgrateLevel);


      }
    }
    yield return new WaitForSeconds(.1f);
    //AnimStop();
  }

  public void RadiusDisplay()
  {
    radius.material.SetFloat("_Display", 1);
  }
  public void RadiusHide()
  {
    radius.material.SetFloat("_Display", showRadius.Value ? 1 : 0);
  }
  public void RadiusToggle()
  {
    radius.material.SetFloat("_Display", showRadius.Value ? 1 : 0);
  }
  public void sellTower()
  {
    Destroy(gameObject);
  }


  void AnimPlay(GameObject obj)
  {
    lineRenderer.SetPosition(0, transform.position);
    lineRenderer.SetPosition(1, obj.transform.position);

    shotPoint.transform.LookAt(obj.transform);

  }
  void AnimStop()
  {
    lineRenderer.SetPosition(0, transform.position);
    lineRenderer.SetPosition(1, transform.position);
    //shotPoint.Stop();
    //muzzle.Stop();
  }
}
