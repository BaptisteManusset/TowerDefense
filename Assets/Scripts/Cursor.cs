using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    private Camera cam;
    Vector3 clickPosition = Vector3.zero;
    [BoxGroup("Dimension")] public int caseSize = 3;
    [BoxGroup("Gizmos")] public GameObject gizmos;
    [BoxGroup("Gizmos")] public Color Sucess;
    [BoxGroup("Gizmos")] public Color Error;
    [BoxGroup("Gizmos")] [ReadOnly] [Label("Position du curseur")] public Vector3 pos;

    [BoxGroup("Tower")] [ShowAssetPreview] [ReadOnly] public GameObject tower;
    [BoxGroup("Tower")] [SerializeField] GameObject parent;
    [BoxGroup("Tower")] [SerializeField] LayerMask m_LayerMask;
    [BoxGroup("Tower")] [SerializeField] LayerMask RaycastLayerMask;
    [BoxGroup("Tower")] [SerializeField] [Tag] string zoneDePlacement;
    [BoxGroup("Tower")] [SerializeField] [Tag] string towerTag;
    [BoxGroup("Tower UI")] [SerializeField] GameObject towerUi;
    [BoxGroup("Tower UI")] [SerializeField] Vector3 offsetTowerUi;



    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // verification de si le curseur est sur le GUI
        if (EventSystem.current.IsPointerOverGameObject() == false)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, RaycastLayerMask))
            {

                Debug.Log(hit.collider.gameObject.tag, hit.collider.gameObject);
                clickPosition = hit.point;
                // convertion des coordonnées en coordonées alignées à la grille
                pos = new Vector3(Mathf.Round(clickPosition.x / caseSize) * caseSize, 2, Mathf.Round(clickPosition.z / caseSize) * caseSize);


                if (hit.collider.gameObject.CompareTag(towerTag))
                {
                    towerUi.transform.position = pos;
                    towerUi.transform.LookAt(Camera.main.transform.position);

                }
                else if (hit.collider.gameObject.tag == zoneDePlacement)
                {
                    
                    gizmos.transform.position = pos;

                    #region verification de si la place est disponible et changement de couleur
                    Collider[] hitColliders = Physics.OverlapBox(pos, Vector3.one, Quaternion.identity, m_LayerMask);
                    if (hitColliders.Length == 0)
                    {
                        gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Sucess);
                        if (Input.GetMouseButtonDown(0))
                        {
                            Instantiate(tower, pos, Quaternion.identity, parent.transform);
                        }
                    }
                    else
                    {
                        gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Error);
                    }
                    #endregion

                }
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


    public void selectTower(GameObject obj)
    {
        tower = obj;
    }
}
