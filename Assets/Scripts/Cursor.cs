using NaughtyAttributes;
using ScriptableVariable.Unite2017.Events;
using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cursor : MonoBehaviour
{
    private Camera cam;
    Vector3 clickPosition = Vector3.zero;
    [BoxGroup("Argent")] public FloatVariable argent;
    [BoxGroup("Shop")] public BoolVariable shopIsOpen;



    [BoxGroup("Dimension")] [ReadOnly] public int caseSize = 3;

    [BoxGroup("Gizmos")] public GameObject gizmos;
    Renderer gizmosRender;
    [BoxGroup("Gizmos")] public Color Sucess;
    [BoxGroup("Gizmos")] public Color Error;
    [BoxGroup("Gizmos")] [ReadOnly] [Label("Position du curseur")] public Vector3 pos;
    [BoxGroup("Gizmos")] public float safeRadius;

    [BoxGroup("Tower")] [ReadOnly] public GameObjectVariable tower;
    [BoxGroup("Tower")] [SerializeField] GameObject parent;
    [BoxGroup("Tower")] [SerializeField] LayerMask m_LayerMask;
    [BoxGroup("Tower")] [SerializeField] LayerMask safeRadius_LayerMask;
    [BoxGroup("Tower")] [SerializeField] LayerMask RaycastLayerMask;
    [BoxGroup("Tower")] [SerializeField] [Tag] string zoneDePlacement;
    [BoxGroup("Tower")] [SerializeField] [Tag] string towerTag;


    [BoxGroup("Tower UI")] [SerializeField] GameObject towerUi;
    [BoxGroup("Tower UI")] [SerializeField] Vector3 offsetTowerUi;
    [BoxGroup("Tower UI")] [SerializeField] GameObjectVariable actualTower;



    void Awake()
    {
        cam = Camera.main;
        gizmosRender = gizmos.GetComponent<Renderer>();
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

                clickPosition = hit.point;

                // align coordinate to the grid
                pos = new Vector3(Mathf.Round(clickPosition.x / caseSize) * caseSize, hit.point.y, Mathf.Round(clickPosition.z / caseSize) * caseSize);


                #region collide with a tower

                if (hit.collider.gameObject.CompareTag(towerTag))
                {
                    //gizmos.SetActive(false);
                    gizmosRender.enabled = false;
                    actualTower.SetValue(hit.collider.gameObject.GetComponent<Tower>().gameObject);

                    //towerUi.SetActive(true);

                    gizmosRender.enabled = true;
                    towerUi.transform.position = pos + offsetTowerUi;
                    towerUi.transform.rotation = Camera.main.transform.rotation;

                }
                #endregion
                else
                {
                    towerUi.SetActive(false);
                    //si on touche une zone de placement
                    if (hit.collider.gameObject.tag == zoneDePlacement)
                    {
                        //actualTower.Clear();

                        //gizmos.SetActive(true);



                        if (shopIsOpen.Value == true)
                        {
                            gizmosRender.enabled = true;
                            gizmos.transform.position = pos;
                            #region verification de si la place est disponible et changement de couleur
                            Collider[] hitCollidersBox = Physics.OverlapBox(pos, Vector3.one, Quaternion.identity, m_LayerMask);
                            if (hitCollidersBox.Length == 0)
                            {
                                Collider[] hitCollidersSphere = Physics.OverlapSphere(pos, safeRadius, safeRadius_LayerMask);
                                if (hitCollidersSphere.Length == 0)
                                {
                                    gizmos.GetComponent<Renderer>().material.SetColor("_BaseColor", Sucess);
                                    gizmos.GetComponentInChildren<Renderer>().material.SetColor("_BaseColor", Sucess);
                                    if (Input.GetMouseButtonDown(0))
                                    {
                                        #region if player have enought money he can place the tower
                                        int buycost = (int)tower.Value.GetComponent<Tower>().statDefault.buyCost;
                                        if (argent.Value - buycost >= 0)
                                        {
                                            argent.ApplyChange(-buycost);

                                            GameObject obj = Instantiate(tower.Value, pos, Quaternion.identity, parent.transform);

                                            obj.gameObject.name += obj.GetHashCode();
                                        }
                                        #endregion
                                    }
                                }
                            }
                            #endregion
                        }

                    }
                }
            }

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(clickPosition, .5f);
        Gizmos.DrawWireSphere(clickPosition, safeRadius);
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
        tower.SetValue(obj);
    }
}
