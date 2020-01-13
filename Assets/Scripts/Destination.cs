using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destination : MonoBehaviour
{
    public FloatVariable vie;
    public LayerMask layer;
    public UnityEvent DamageEvent;
    public UnityEvent DeathEvent;
    private void OnTriggerEnter(Collider other)
    {

        Mind ennemy = other.GetComponent<Mind>();
        vie.ApplyChange(-ennemy.damageToTower);


        if (vie.Value <= 0)
        {
            DeathEvent.Invoke();
        }
        else
        {
            DamageEvent.Invoke();
        }

        Destroy(other.gameObject);
    }
}
