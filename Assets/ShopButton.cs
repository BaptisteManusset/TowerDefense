using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class ShopButton : MonoBehaviour
{

    [ShowAssetPreview] public GameObject target;
    MainShop mainShop;
    void Awake()
    {
        mainShop = FindObjectOfType<MainShop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
