using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class gridDisplay : MonoBehaviour
{
    public int GridSize;
    public int caseSize = 3;
    public float hauteur;
    public bool display;
    public float offset = 1.5f;
    void OnDrawGizmos()
    {
        if (display)
        {
            Gizmos.color = Color.red;


            for (int i = 0; i < GridSize; i++)
            {
                Gizmos.DrawLine(
                  new Vector3(i * caseSize - offset, hauteur, 0 - offset),
                  new Vector3(i * caseSize - offset, hauteur, GridSize * caseSize - offset));

                Gizmos.DrawLine(
                  new Vector3(0 - offset, hauteur, i * caseSize - offset),
                  new Vector3(GridSize * caseSize - offset, hauteur, i * caseSize - offset));

            }
            Gizmos.DrawLine(
              new Vector3(GridSize * caseSize - offset, hauteur, 0 - offset),
              new Vector3(GridSize * caseSize - offset, hauteur, GridSize * caseSize - offset));

            Gizmos.DrawLine(
              new Vector3(0 - offset, hauteur, GridSize * caseSize - offset),
              new Vector3(GridSize * caseSize - offset, hauteur, GridSize * caseSize - offset));

        }
    }
}
