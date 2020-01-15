using NaughtyAttributes;
using ScriptableVariable.Unite2017.Events;
using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    private Camera cam;
    Vector3 clickPosition = Vector3.zero;
    [BoxGroup("Argent")] [SerializeField] FloatVariable argent;



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
    [BoxGroup("Tower UI")] [SerializeField] GameObjectVariable actualTower;



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

                //Debug.Log(hit.collider.gameObject.tag, hit.collider.gameObject);
                clickPosition = hit.point;
                // align coordinate to the grid
                pos = new Vector3(Mathf.Round(clickPosition.x / caseSize) * caseSize, 2, Mathf.Round(clickPosition.z / caseSize) * caseSize);



                if (hit.collider.gameObject.CompareTag(towerTag))
                {
                    gizmos.SetActive(false);
                    actualTower.SetValue(hit.collider.gameObject.GetComponent<Tower>().gameObject);

                    towerUi.SetActive(true);
                    towerUi.transform.position = pos + offsetTowerUi;
                    towerUi.transform.rotation = Camera.main.transform.rotation;

                }
                else
                {
                    towerUi.SetActive(false);
                    //si on touche une zone de placement
                    if (hit.collider.gameObject.tag == zoneDePlacement)
                    {
                        //actualTower.Clear();

                        gizmos.SetActive(true);
                        gizmos.transform.position = pos;

                        #region verification de si la place est disponible et changement de couleur
                        Collider[] hitColliders = Physics.OverlapBox(pos, Vector3.one, Quaternion.identity, m_LayerMask);
                        if (hitColliders.Length == 0)
                        {
                            gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Sucess);
                            if (Input.GetMouseButtonDown(0))
                            {
                                #region if player have enought money he can place the tower
                                int buycost = (int)tower.GetComponent<Tower>().statDefault.buyCost;
                                if (argent.Value - buycost >= 0)
                                {
                                    argent.ApplyChange(-buycost);

                                    var obj = Instantiate(tower, pos, Quaternion.identity, parent.transform);

                                    obj.gameObject.name += obj.GetHashCode();
                                }
                                #endregion
                            }
                        }
                        #endregion

                    }
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
    void changeColor()
    {
        gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Error);
    }


    public void selectTower(GameObject obj)
    {
        tower = obj;
    }
}
