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
        Debug.Log("FixedUpdate Mine");

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

    protected override IEnumerator Shot()
    {
        Debug.Log("Shot Mine");
        shootInProgress = true;
        if (reloadRequire == false)
        {
            reloadRequire = true;
            shotLoading = 0;
            transform.LookAt(GetClosestTarget(GetTargets()));
            transform.Translate(transform.forward);

        }
        yield return new WaitForSeconds(.1f);
        shootInProgress = false;
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
