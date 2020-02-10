using NaughtyAttributes;
using UnityEngine;
public class MainShop : MonoBehaviour
{
    [BoxGroup("Tour Disponible")] [SerializeField] GameObject[] itemToSell;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject prefab;


    void Awake()
    {
        foreach (var item in GetComponentsInChildren<ShopButton>())
        {
            Destroy(item.gameObject);
        }
    }

    void Start()
    {
        UpdateUi();
    }

    [Button]
    void UpdateUi()
    {
        for (int i = 0; i < itemToSell.Length; i++)
        {
            GameObject p = Instantiate(prefab, parent.transform);
            p.GetComponent<ShopButton>().SetObj(itemToSell[i].gameObject);
        }
    }
}
