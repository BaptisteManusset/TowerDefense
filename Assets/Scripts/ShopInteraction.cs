using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class ShopInteraction : MonoBehaviour
{
  Placer placer;
  [ShowAssetPreview] public GameObject target;
  [SerializeField] int valeur;
  [SerializeField] TextMeshProUGUI valeurTxt;
  void Awake()
  {
    placer = FindObjectOfType<Placer>();
    valeurTxt.text = valeur + "C";
  }
  public void SelectObject()
  {
    placer.tower = target;
  }
}
