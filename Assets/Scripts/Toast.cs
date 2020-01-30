using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Toast : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleUi;
    [SerializeField] TextMeshProUGUI contentUi;
    public void SetMessage(string title, string content)
    {
        titleUi.SetText(title);
        contentUi.SetText(title);
    }

    public void test()
    {

    }
}
