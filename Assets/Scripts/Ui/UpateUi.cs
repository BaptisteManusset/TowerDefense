using NaughtyAttributes;
using RoboRyanTron.Unite2017.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpateUi : MonoBehaviour
{
  public FloatReference valeur;

  public FloatReference max;
  [Label("Vies")] [SerializeField] private TextMeshProUGUI texte;
  [Label("Background")] [SerializeField] private Image image;

  void Update()
  {
    if (texte) texte.text = Mathf.Clamp(Mathf.CeilToInt(valeur.Value), 0, max.Value).ToString();
    if (image) image.fillAmount = valeur.Value / max.Value;

  }
}
