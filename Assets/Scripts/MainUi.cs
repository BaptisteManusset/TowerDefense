using NaughtyAttributes;
using ScriptableVariable.Unite2017.Sets;
using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.Events;

public class MainUi : MonoBehaviour
{

    [SerializeField] ThingRuntimeSet towers;
    [SerializeField] BoolVariable showRadius;

    [SerializeField] UnityEvent initShop;


    void Awake()
    {
        showRadius.SetValue(true);

        ToggleRadius();
    }

    void Start()
    {
        initShop.Invoke();
    }

    public void ToggleRadius()
    {
        showRadius.Invert();
        for (int i = 0; i < towers.Items.Count; i++)
        {
            towers.Items[i].GetComponent<Tower>().RadiusToggle();
            //radius.material.SetFloat("_Display", showRadius.Value ? 1 : 0);
        }

    }

}
