using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiUpdateText : MonoBehaviour
{
  public FloatReference valeur;
  [Label("Vies")] [SerializeField] private TextMeshProUGUI texte;
  [BoxGroup("Interface")] public string prefix;
  [BoxGroup("Interface")] public string suffix;

    [Button]
  void Update()
  {
    texte.text = prefix + valeur.Value + suffix;
  }
}
