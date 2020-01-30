using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{

  [SerializeField] Transform target;
  int interval = 3;
  void Update()
  {
    if (Time.frameCount % interval == 0)
    {
      transform.LookAt(target);
    }
  }
}
