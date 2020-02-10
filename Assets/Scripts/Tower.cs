
using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tower : MonoBehaviour
{


    Color color = Color.red;
    [BoxGroup("Target"), Label("Mob détectés")] public List<Mind> targets;
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

    [SerializeField] Transform rotatePoint;


    private LineRenderer lineRenderer;
    bool shootInProgress = false;
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


        if (shootInProgress == false)
        {

            #region Clear Targets
            for (var y = GetTargets().Count - 1; y > -1; y--)
            {
                if (targets[y] == null)
                    targets.RemoveAt(y);
            }
            #endregion





            Collider[] tempArray = Physics.OverlapSphere(transform.position, stat.datas["Radius"].value, layerMask);


            if (stat.isZoneAttack == false)
            {
                int i = 0;
                while (i < tempArray.Length && GetTargets().Count == 0)
                {
                    Mind mind = tempArray[i].GetComponent<Mind>();
                    if (mind)
                    {
                        if (mind.IsDead() == false)
                        {
                            GetTargets().Add(mind);
                        }
                    }
                    i++;
                }
            }
            else
            {
                for (int i = 0; i < tempArray.Length; i++)
                {
                    Mind mind = tempArray[i].GetComponent<Mind>();
                    if (mind)
                    {
                        if (mind.IsDead() == false)
                        {
                            GetTargets().Add(mind);
                        }
                    }
                }


            }
            if (GetTargets().Count > 0)
            {
                StartCoroutine(Shot());
            }


        }
        else
        {
            if (reloadRequire)
            {

                if (shotLoading >= 100)
                {
                    shotLoading = 100;
                    reloadRequire = false;
                }
                else
                {
                    shotLoading += stat.datas["ReloadSpeed"].upgrateLevel * stat.datas["ReloadSpeed"].value;
                }
            }
        }

        if (GetTargets().Count > 0)
        {
            AnimPlay(GetTargets()[0].gameObject);
        }
    }
    void OnDrawGizmos()
    {

        Gizmos.color = color;
        if (GetTargets().Count >= 0)
        {
            for (int i = 0; i < GetTargets().Count; i++)
            {
                Mind m = targets[i]?.GetComponent<Mind>();
                if (m == null) continue;

                Gizmos.DrawLine(transform.position, targets[i].transform.position);
                DebugExtension.DrawArrow(transform.position, targets[i].transform.position - transform.position, Color.yellow);

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
        shootInProgress = true;
        if (reloadRequire == false)
        {
            if (!shotPoint.isPlaying)
                shotPoint.Play();

            if (!muzzle.isPlaying)
                muzzle.Play();



            for (int i = 0; i < GetTargets().Count; i++)
            {
                AnimPlay(GetTargets()[i].gameObject);
            }

            Debug.Log("sHot");

            yield return new WaitForSeconds(1f);


            reloadRequire = true;
            shotLoading = 0;

            for (int i = 0; i < GetTargets().Count; i++)
            {
                GetTargets()[i].Damage(stat.datas["Damage"].value * stat.datas["Damage"].upgrateLevel);
            }

        }
        yield return new WaitForSeconds(.1f);
        shootInProgress = false;
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
        if (stat.isZoneAttack)
        {


        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, obj.transform.position);

            rotatePoint.LookAt(obj.transform);
        }
    }
    void AnimStop()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
        //shotPoint.Stop();
        //muzzle.Stop();
    }

    List<Mind> GetTargets()
    {
        if (stat.isZoneAttack == false)
        {
            if (targets.Count > 1)
                targets.RemoveRange(1, targets.Count - 1);
        }

        return targets;

    }
}
