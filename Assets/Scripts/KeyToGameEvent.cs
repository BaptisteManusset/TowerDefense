using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyToGameEvent : MonoBehaviour
{
    [ReorderableList] public keyEvent[] keyEvents;
    void Update()
    {
        for (int i = 0; i < keyEvents.Length; i++)
        {
            if (Input.GetKeyDown(keyEvents[i].key)) keyEvents[i].evnt.Invoke();
        }
    }
}

[System.Serializable]
public class keyEvent
{
    [Label("Nom du groupe")]public string name;
    public KeyCode key;
    public UnityEvent evnt;

}