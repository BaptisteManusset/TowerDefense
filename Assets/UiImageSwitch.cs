using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiImageSwitch : MonoBehaviour
{
  [SerializeField] Sprite on;
  [SerializeField] Sprite off;
  [SerializeField] Image image;
  [SerializeField] BoolVariable showRadius;


  public void SwitchSprite()
  {
    image.sprite = showRadius.Value ? on : off;
  }

  [Button("inverser les images")]
  void Invert()
  {
    Sprite temp;
    temp = on;
    on = off;
    off = temp;

  }

}
