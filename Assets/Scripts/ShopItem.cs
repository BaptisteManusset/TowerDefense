using NaughtyAttributes;
using TMPro;
using UnityEngine;
using itsbaptiste;
public class ShopItem : MonoBehaviour
{
    [BoxGroup("Info")] [SerializeField] string nom;
    [BoxGroup("Info")] [SerializeField] GameObject obj;
    [BoxGroup("Info")] [SerializeField] int tarif;

    [BoxGroup("Technique")] [SerializeField] TextMeshProUGUI TxtNom;
    [BoxGroup("Technique")] [SerializeField] TextMeshProUGUI TxtTarif;
    itsbaptiste.Cursor placer;

    [Button("Update")]
    void Start()
    {
        TxtNom.text = nom;
        TxtTarif.text = tarif + "#";
        placer = (itsbaptiste.Cursor)FindObjectOfType(typeof(itsbaptiste.Cursor));
        Debug.Log("shop item", gameObject);

    }

    public void Click()
    {
        placer.tower.SetValue(obj);
    }
}
