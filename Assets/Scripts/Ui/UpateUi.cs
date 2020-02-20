using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpateUi : MonoBehaviour
{
    public FloatReference valeur;

    public FloatReference max;
    [Label("Vies")] [SerializeField] private TextMeshProUGUI texte;
    [Label("Background")] [SerializeField] private Image image;

    [SerializeField] string preffix;
    [SerializeField] string suffix;
    void Update()
    {
        if (texte) texte.text = preffix + Mathf.Clamp(Mathf.CeilToInt(valeur.Value), 0, max.Value) + suffix;
        if (image) image.fillAmount = valeur.Value / max.Value;

    }
}
