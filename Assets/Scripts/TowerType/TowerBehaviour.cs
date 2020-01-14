using NaughtyAttributes;
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
            Mind m = master.GetTarget().GetComponent<Mind>();
            m.Damage(master.stat.damage);
            Shotenabled = false;
            shotLoading = 0;
            if (m.IsDead())
            {
                master.DefineTarget();
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
