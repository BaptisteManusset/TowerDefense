using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
  public FloatVariable vie;
  public LayerMask layer;
  private void OnTriggerEnter(Collider other)
  {
    vie.ApplyChange(-other.GetComponent<Mind>().damageToTower);
    Destroy(other.gameObject);
  }
}
