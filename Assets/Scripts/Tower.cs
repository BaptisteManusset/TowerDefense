using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class Tower : MonoBehaviour
{


    Color color = Color.red;
    [BoxGroup("Target"), Label("Mob détectés")] public List<Mind> targets;
    [BoxGroup("Target"), Label("Layer des mobs")] public LayerMask layerMask;


    // Must be public
    [BoxGroup("Stats")] public TowerStat statDefault;
    [BoxGroup("Stats")] public TowerStat stat;


    [SerializeField] protected Renderer radius;
    [SerializeField] protected int radiusDefault = 25;

    [BoxGroup("Reload")] [SerializeField] [Label("Tir prés")] protected bool reloadRequire = false;
    [BoxGroup("Reload")] [ProgressBar("chargement")] public float shotLoading = 100;

    [BoxGroup("FX")] [SerializeField] [Required] protected ParticleSystem shotPoint;
    [BoxGroup("FX")] [SerializeField] [Required] protected ParticleSystem muzzle;
    [BoxGroup("FX")] [SerializeField] [Required] protected ParticleSystem loading;


    [BoxGroup("Externe")] [SerializeField] protected BoolVariable showRadius;

    [SerializeField] [Required] protected Transform rotatePoint;

    private AudioSource audio;

    

    //protected LineRenderer lineRenderer;
    protected bool shootInProgress = false;
    void Awake()
    {
        stat = Object.Instantiate(statDefault);
        stat.Init();
        audio = GetComponent<AudioSource>();
        radius.material.SetFloat("_Display", showRadius.Value ? 1 : 0);

    }
    void Start()
    {
        UpdateInfo();
    }
    protected virtual void FixedUpdate()
    {
        if (shootInProgress == false)
        {
            ClearTargets();

            if (Seek())
                StartCoroutine(Shot());
        }
        else
        {
            Reload();
        }

        if (GetTargets().Count > 0)
        {
            AnimPlay();
        }
    }

    protected bool Seek()
    {

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

        return targets.Count > 0 ? true : false;
    }
    protected void ClearTargets()
    {
        for (var y = GetTargets().Count - 1; y > -1; y--)
        {
            if (targets[y] == null)
                targets.RemoveAt(y);
        }
    }
    protected List<Mind> GetTargets()
    {
        if (stat == null) return null;

        if (stat.isZoneAttack == false)
        {
            if (targets.Count > 1)
                targets.RemoveRange(1, targets.Count - 1);
        }

        return targets;

    }
    protected virtual IEnumerator Shot()
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
                AnimPlay(i);
            }


            yield return new WaitForSeconds(1f);

            audio.Play();

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
    protected void Reload()
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
    #region Public method for display 
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
    public void UpdateInfo()
    {
        int scale = stat.datas["Radius"].upgrateLevel * 8 + radiusDefault;

        radius.transform.localScale = Vector3.one * scale;
        scale /= 2; // diameter to radius
        stat.datas["Radius"].value = scale;
    }

    #endregion
    public void sellTower()
    {
        Destroy(gameObject);
    }
    protected void AnimPlay(int i = 0)
    {
        if (GetTargets()[i] == null) return;
        GameObject obj = GetTargets()[i].gameObject;
        if (stat.isZoneAttack == false)
        {
            //lineRenderer.SetPosition(0, shotPoint.transform.position);
            //lineRenderer.SetPosition(1, obj.transform.position);

            rotatePoint.LookAt(obj.transform);
        }
    }
    protected void AnimStop()
    {
        //lineRenderer.SetPosition(0, transform.position);
        //lineRenderer.SetPosition(1, transform.position);
    }
    protected void OnDrawGizmos()
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

}
