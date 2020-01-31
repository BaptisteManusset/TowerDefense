using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //private EventSystem eventSystem;
    //private bool cursorInside = false;
    //private bool isUIObject = false;
    //private bool showing = false;

    //private void Awake()
    //{
    //    //eventSystem = FindObjectOfType<EventSystem>();
    //    //tooltipController = FindObjectOfType<STController>();

    //    //// Add a new tooltip prefab if one does not exist yet
    //    //if (!tooltipController)
    //    //{
    //    //    tooltipController = AddTooltipPrefabToScene();
    //    //}
    //    //if (!tooltipController)
    //    //{
    //    //    Debug.LogWarning("Could not find the Tooltip prefab");
    //    //    Debug.LogWarning("Make sure you don't have any other prefabs named `SimpleTooltip`");
    //    //}

    //    //if (GetComponent<RectTransform>())
    //    //    isUIObject = true;

    //}

    private void Update()
    {
        //if (!cursorInside)
        //    return;
    }

    //public static STController AddTooltipPrefabToScene()
    //{
    //    return Instantiate(Resources.Load<GameObject>("SimpleTooltip")).GetComponentInChildren<STController>();
    //}

    private void OnMouseOver()
    {
        //if (isUIObject)
        //    return;

        //if (eventSystem)
        //{
        //    if (eventSystem.IsPointerOverGameObject())
        //    {
        //        HideTooltip();
        //        return;
        //    }
        //}
        ShowTooltip();
    }

    private void OnMouseExit()
    {
        //if (isUIObject)
        //    return;
        HideTooltip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //if (!isUIObject)
        //    return;
        ShowTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //    if (!isUIObject)
        //        return;
        HideTooltip();
    }

    public void ShowTooltip()
    {
        //showing = true;
        //cursorInside = true;

        Debug.Log("SHOW TOOLTIP");

        // Update the text for both layers
        //tooltipController.SetCustomStyledText(infoLeft, simpleTooltipStyle, STController.TextAlign.Left);
        //tooltipController.SetCustomStyledText(infoRight, simpleTooltipStyle, STController.TextAlign.Right);

        //// Then tell the controller to show it
        //tooltipController.ShowTooltip();
    }

    public void HideTooltip()
    {

        Debug.Log("HIDE TOOLTIP");

        //if (!showing)
        //    return;
        //showing = false;
        //cursorInside = false;
        //tooltipController.HideTooltip();
    }
}
