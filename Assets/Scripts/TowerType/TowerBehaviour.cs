using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [HideInInspector] public Tower master;


    bool Shotenabled = true;
    [BoxGroup("Stats"), ProgressBar("chargement")] public float shotLoading = 100;

    [BoxGroup("Stats"), Label("Vitesse de recharge")] public float shotStepLoad = 1;

    public void Shot()
    {
        if (Shotenabled)
        {
            Mind m;
            List<GameObject> obj = master.GetTarget();
            for (int i = 0; i < obj.Count; i++)
            {
                m = null;
                m = obj[i].GetComponent<Mind>();
                if (m == null) Debug.LogError("Cette ennemie n'a pas de Mind", obj[i]);

                m.Damage(master.stat.datas["Damage"].value);
                Shotenabled = false;
                shotLoading = 0;
                if (m.IsDead())
                {
                    master.DefineTarget();
                }
            }
        }
    }

    public void ShotReload()
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
