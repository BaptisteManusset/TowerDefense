using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
  private Camera cam;
  Vector3 clickPosition = Vector3.zero;
  [BoxGroup("Dimension")] public int caseSize = 3;
  [BoxGroup("Gizmos")] public GameObject gizmos;
  [BoxGroup("Gizmos")] public Color Sucess;
  [BoxGroup("Gizmos")] public Color Error;
  [BoxGroup("Gizmos")] [ReadOnly] [Label("Position du curseur")] public Vector3 pos;

  [BoxGroup("Tower")] [SerializeField] GameObject tower;
  [BoxGroup("Tower")] [SerializeField] GameObject parent;
  [BoxGroup("Tower")] [SerializeField] LayerMask m_LayerMask;
  void Start()
  {
    cam = Camera.main;
  }

  void Update()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit))
    {
      clickPosition = hit.point;
      pos = new Vector3(
     Mathf.Round(clickPosition.x / caseSize) * caseSize,
     5,
     Mathf.Round(clickPosition.z / caseSize) * caseSize);

      #region selection de la case
      gizmos.transform.position = pos;
      #endregion


      Collider[] hitColliders = Physics.OverlapBox(pos, Vector3.one, Quaternion.identity, m_LayerMask);
      if (hitColliders.Length == 0)
      {
        gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Sucess);
        if (Input.GetMouseButtonDown(0))
        {
          Instantiate(tower, pos, Quaternion.identity, parent.transform);
        }
      } else
      {
        gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Error);

        Debug.Log("Placement impossible");
      }

    }

  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(clickPosition, .5f);
    Gizmos.DrawLine(transform.position, clickPosition);

    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(pos, 2);
  }
  [Button]
  public void changeColor()
  {
    gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Error);
  }
}
