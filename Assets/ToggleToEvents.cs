using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleToEvents : MonoBehaviour
{
    [SerializeField] UnityEvent on;
    [SerializeField] UnityEvent off;
    public void Toggle(bool value)
    {
        if (value) { on.Invoke(); }
        else { off.Invoke(); }
    }
}
