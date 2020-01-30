using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uiStatDisplayDot : MonoBehaviour
{
    [SerializeField] [MinValue(0), MaxValue(6)] private int value;
    [SerializeField] Image[] dots;
    [SerializeField] TextMeshProUGUI nameUI;
    void Start()
    {
        //name
    }
    [Button]
    void UpdateDot()
    {
        for (int i = 0; i < dots.Length; i++)
        {
            if (i < value)
            {
                dots[i].color = Color.green;
            }
            else
            {
                dots[i].color = Color.grey;
            }
        }
    }

    public void UpdateUi(int v, string n)
    {
        value = v;
        name = n;

        nameUI.text = name;
        UpdateDot();
    }
}
