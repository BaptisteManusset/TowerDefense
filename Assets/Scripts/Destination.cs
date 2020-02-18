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
  [SerializeField][Tooltip("Activer les degas fait au CORE")] bool enable = true;

  private void OnTriggerEnter(Collider other)
  {



    Mind ennemy = other.GetComponent<Mind>();
    if (!ennemy) return;

    vie.ApplyChange(-ennemy.damageToTower);

    if (enable)
    {
      if (vie.Value <= 0)
      {
        DeathEvent.Invoke();
      } else
      {
        DamageEvent.Invoke();
      }
    }

    Destroy(other.gameObject);
  }
}
