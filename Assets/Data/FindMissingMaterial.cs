using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class FindMissingMaterial : MonoBehaviour
{
    [Button("Tools/Do Something")]
    public void dflkfjlfgjkfddfl()
    {
        Debug.Log("rechercher");
        MeshRenderer[] meshs = GameObject.FindObjectsOfType<MeshRenderer>();

        for (int i = 0; i < meshs.Length; i++)
        {
            MeshRenderer slt = meshs[i];

            for (int y = 0; y < slt.materials.Length; y++)
            {
                if(slt.materials[y] == null)
                {
                    Debug.LogError("Material missing on this object !",slt.gameObject);
                }
            }
        }
    }
}
