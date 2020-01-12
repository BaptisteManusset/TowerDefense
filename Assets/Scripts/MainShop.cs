using NaughtyAttributes;
using UnityEngine;

public class MainShop : MonoBehaviour
{
  [ReadOnly] public GameObject selectedTurrel;
  [ReadOnly] public Placer placer;

  void Awake()
  {
    placer = FindObjectOfType<Placer>();
  }

}
