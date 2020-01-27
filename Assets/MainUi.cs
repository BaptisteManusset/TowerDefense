using NaughtyAttributes;
using ScriptableVariable.Unite2017.Sets;
using UnityEngine;
using UnityEngine.Events;

public class MainUi : MonoBehaviour
{

  [SerializeField] ThingRuntimeSet towers;

  [SerializeField] UnityEvent initShop;

  void Start()
  {
    initShop.Invoke();
  }

  public void ToggleRadius(bool value)
  {
    for (int i = 0; i < towers.Items.Count; i++)
    {
      towers.Items[i].transform.Find("Radius").gameObject.SetActive(value);
    }

  }

}
