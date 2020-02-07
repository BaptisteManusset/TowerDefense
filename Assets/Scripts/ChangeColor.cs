using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

  public List<MeshRenderer> renders;

  [Button]
  void UpdateColor()
  {
    renders = new List<MeshRenderer>(GetComponentsInChildren<MeshRenderer>());
    renders.Add(GetComponent<MeshRenderer>());

    for (int i = 0; i < renders.Count; i++)
    {
      renders[i].material.color = Color.green;
    }
  }
}
