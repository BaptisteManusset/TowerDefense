using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [BoxGroup("Info")] [SerializeField] string nom;
    [BoxGroup("Info")] [SerializeField] GameObject obj;
    [BoxGroup("Info")] [SerializeField] int tarif;

    [BoxGroup("Technique")] [SerializeField] TextMeshProUGUI TxtNom;
    [BoxGroup("Technique")] [SerializeField] TextMeshProUGUI TxtTarif;
    Cursor placer;

    [Button("Update")]
    void Start()
    {
        TxtNom.text = nom;
        TxtTarif.text = tarif + "#";
        placer = (Cursor)FindObjectOfType(typeof(Cursor));


    }

    public void Click()
    {
        placer.tower.SetValue(obj);
    }
}
