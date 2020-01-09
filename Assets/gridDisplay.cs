using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[ExecuteInEditMode]
public class gridDisplay : MonoBehaviour
{
    public int GridSize;
    public int caseSize = 3;
    public int hauteur;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;


        for (int i = 0; i < GridSize; i++)
        {
            Gizmos.DrawLine(new Vector3(i * caseSize, hauteur, 0), new Vector3(i * caseSize, hauteur, GridSize * caseSize));

            Gizmos.DrawLine(new Vector3(0, hauteur, i * caseSize), new Vector3(GridSize * caseSize, hauteur, i * caseSize));

        }
        Gizmos.DrawLine(new Vector3(GridSize * caseSize, hauteur, 0), new Vector3(GridSize * caseSize, hauteur, GridSize * caseSize));

        Gizmos.DrawLine(new Vector3(0, hauteur, GridSize * caseSize), new Vector3(GridSize * caseSize, hauteur, GridSize * caseSize));

    }
}
