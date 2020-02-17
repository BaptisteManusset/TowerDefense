using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(WaveManager))]
[CanEditMultipleObjects]
public class WaveManagerEditor : Editor
{
  public override void OnInspectorGUI() //2
  {

    WaveManager wave = (WaveManager)target;
    GUILayout.BeginHorizontal();
    wave.test = EditorGUILayout.IntField( wave.test);
  EditorGUILayout.LabelField("dsfdf");

    GUILayout.EndHorizontal();
  }
}
