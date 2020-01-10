using NaughtyAttributes;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{
    [HideInInspector] public Tower master;


    bool Shotenabled = true;
    [BoxGroup("Stats"), ProgressBar("chargement")] public float shotLoading = 100;

    [BoxGroup("Stats"), Label("Vitesse de recharge")] public float shotStepLoad = 1;
    [BoxGroup("Stats")] public int damage = 10;

    [Space(10)][BoxGroup("Stats")] public float detectionRadius = 10;

    public void Test()
    {
        Debug.Log("Chargement Reussie !", gameObject);
    }


    public void Shot()
    {
        if (Shotenabled)
        {
            Mind m = master.GetTarget().GetComponent<Mind>();
            m.Damage(damage);
            Shotenabled = false;
            shotLoading = 0;
            if (m.IsDead()) master.targetInt = -1;
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
            shotLoading += shotStepLoad * Time.deltaTime * 100;
        }
    }
}
