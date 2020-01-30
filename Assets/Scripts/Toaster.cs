using UnityEngine;
using TMPro;
public class Toaster : MonoBehaviour
{

    [SerializeField] GameObject toastPrefab;


    public void SetMessage(string title, string content)
    {
        GameObject obj = Instantiate(toastPrefab, transform);
        Toast toast = obj.GetComponent<Toast>();
        toast.SetMessage(title, content);
    }
}
