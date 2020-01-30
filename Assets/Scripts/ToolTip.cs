using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("On Mouse Over");
    }

    void OnMouseExit()
    {
        Debug.Log("On Mouse Exit");

    }
}
