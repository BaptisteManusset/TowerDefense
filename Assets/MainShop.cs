using NaughtyAttributes;
using UnityEngine;

public class MainShop : MonoBehaviour
{
    [BoxGroup("Tour Disponible")] [ReorderableList] [SerializeField] GameObject[] itemToSell;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject prefab;


    void Awake()
    {
        Destroy(GetComponentInChildren<ShopButton>().gameObject);
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
            p.GetComponent<ShopButton>().UpdateUi(itemToSell[i]);
        }
    }
}
