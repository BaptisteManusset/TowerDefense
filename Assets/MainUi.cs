using NaughtyAttributes;
using ScriptableVariable.Unite2017.Sets;
using UnityEngine;

public class MainUi : MonoBehaviour
{

    [SerializeField] ThingRuntimeSet towers;

    public void ToggleRadius(bool value)
    {
        for (int i = 0; i < towers.Items.Count; i++)
        {
            towers.Items[i].transform.Find("Radius").gameObject.SetActive(value);
        }

    }

}
