using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destination : MonoBehaviour
{
  [SerializeField] FloatVariable vie;
  [SerializeField] LayerMask layer;
  [SerializeField] UnityEvent DamageEvent;
  [SerializeField] UnityEvent DeathEvent;
  [SerializeField] bool enableEffect = true;

  private void OnTriggerEnter(Collider other)
  {

    if (!enableEffect) return;

    Mind ennemy = other.GetComponent<Mind>();
    vie.ApplyChange(-ennemy.damageToTower);


    if (vie.Value <= 0)
    {
      DeathEvent.Invoke();
    } else
    {
      DamageEvent.Invoke();
    }

    Destroy(other.gameObject);
  }
}
