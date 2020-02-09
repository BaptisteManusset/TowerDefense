
using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using UnityEngine;

public class MainUpgrade : MonoBehaviour
{
    [SerializeField] GameObjectVariable actualTower;
    [SerializeField] GameObject UiEmpty;
    [SerializeField] GameObject UiNotEmpty;


    void Awake()
    {
        //SetDisplay();
    }

  /// <summary>
  /// toggle on the ui for the screen upgrade if necessary
  /// </summary>
    [Button("Update UI")]
    public void SetDisplay()
    {

        if (actualTower.Value == null)
        {
            UiEmpty.SetActive(true);
            UiNotEmpty.SetActive(false);
        }
        else
        {
            UiEmpty.SetActive(false);
            UiNotEmpty.SetActive(true);
        }
    }


    void Start()
    {
        SetDisplay();
    }


    public void Update()
    {
        //SetDisplay();
    }
}
