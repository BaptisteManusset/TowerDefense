using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : Tower
{
    void Awake()
    {
        stat = Object.Instantiate(statDefault);
        stat.Init();
        lineRenderer = GetComponent<LineRenderer>();
        radius.material.SetFloat("_Display", showRadius.Value ? 1 : 0);
    }
    protected override void FixedUpdate()
    {



        if (shootInProgress == false)
        {
            if (GetTargets().Count > 0)
            {
                ClearTargets();
                Seek();
                Transform closest = GetClosestTarget(GetTargets());
                int dist = (int)Vector3.Distance(closest.position, transform.position);
                if (dist > 1)
                {
                    transform.LookAt(closest);
                    transform.Translate(transform.forward * .5f);
                    Debug.Log("distance  " + dist);
                }
                else
                {
                    StartCoroutine(Shot());
                }
            }
            else
            {
                Seek();
            }
        }
    }



    protected override IEnumerator Shot()
    {
        shootInProgress = true;
        ClearTargets();
        Seek();

        for (int i = 0; i < GetTargets().Count; i++)
        {
            GetTargets()[i].Damage(stat.datas["Damage"].value * stat.datas["Damage"].upgrateLevel);
        }
        yield return new WaitForSeconds(.1f);
        shotPoint.Play();
        Destroy(gameObject);

    }


    protected Transform GetClosestTarget(List<Mind> tgs)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Mind potentialTarget in tgs)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }


}
