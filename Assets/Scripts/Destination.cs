using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public LayerMask layer;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        Main.instance.DecreaseLifes();
    }
}
