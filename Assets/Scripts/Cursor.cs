using NaughtyAttributes;
using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class Cursor : MonoBehaviour
{
    private Camera cam;
    Vector3 clickPosition = Vector3.zero;
    [HideInInspector] public int caseSize = 3;



    [BoxGroup("Argent")] public FloatVariable argent;
    [BoxGroup("Shop")] public BoolVariable shopIsOpen;




    [BoxGroup("Gizmos")] public GameObject gizmos;
    Renderer gizmosRender;

    [BoxGroup("Placement")] [ReadOnly] [Label("Position du curseur")] public Vector3 pos;
    [BoxGroup("Placement")] [Tooltip("Taille de la zone o� le placement est interdit")] public float safeRadius;
    [BoxGroup("Placement")] [SerializeField] [Label("Parent des Tours")] GameObject parent;

    [BoxGroup("Tower Placement")] public GameObjectVariable tower;
    [InfoBox("Liste des �l�ments sur lequelle la tour ne PEUT PAS etre plac�e", EInfoBoxType.Normal)] [Space(20)] [BoxGroup("Tower Placement")] [SerializeField] LayerMask LayerMaskAtPosition; //
    [BoxGroup("Tower Placement")] [SerializeField] [FormerlySerializedAs("safeRadius_LayerMask")] LayerMask safeRadiusMask;
    [BoxGroup("Tower Placement")] [SerializeField] LayerMask RaycastLayerMask;
    [BoxGroup("Tower Placement")] [SerializeField] [Tag] string zoneDePlacement;
    [BoxGroup("Tower Placement")] [SerializeField] [Tag] string towerTag;
    [BoxGroup("Tower Placement")] [SerializeField] [Tag] string restrictedArea;


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
        if (EventSystem.current && EventSystem.current.IsPointerOverGameObject() == false)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000, RaycastLayerMask))
            {

                clickPosition = hit.point;
                if (Vector3Int.CeilToInt(hit.normal) != Vector3.up) return;

                // align coordinate to the grid
                pos = new Vector3(Mathf.Round(clickPosition.x / caseSize) * caseSize, hit.point.y, Mathf.Round(clickPosition.z / caseSize) * caseSize);


                #region collide with a tower

                if (hit.collider.gameObject.CompareTag(towerTag))
                {
                    actualTower.SetValue(hit.collider.gameObject.GetComponent<Tower>().gameObject);
                    towerUi.SetActive(true);
                    towerUi.transform.position = actualTower.Value.transform.position + offsetTowerUi;
                    towerUi.transform.rotation = Camera.main.transform.rotation;
                    gizmos.transform.position = new Vector3(0, -100, 0);
                }
                else
                {
                    #endregion
                    if (shopIsOpen.Value == true)
                    {
                        if (hit.collider.gameObject.CompareTag(restrictedArea) == false)
                        {
                            towerUi.SetActive(false);
                            //si on touche une zone de placement
                            if (hit.collider.gameObject.CompareTag(zoneDePlacement))
                            {
                                if (tower)
                                {
                                    gizmos.transform.position = pos;
                                    #region verification de si la place est disponible
                                    if (PlaceIsFree(pos))
                                    {
                                        if (Input.GetMouseButtonDown(0))
                                        {
                                            #region if player have enought money he can place the tower
                                            if (BuyIfPossible())
                                            {
                                                GameObject obj = Instantiate(tower.Value, pos, Quaternion.identity, parent.transform);
                                            }
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    Debug.LogError("There is no turrel assigned");
                                }
                            }
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


    /// <summary>
    /// assign the selected tower to the gameobjectvariable .
    /// </summary>
    /// <param name="obj">The object.</param>
    public void selectTower(GameObject obj)
    {
        tower.SetValue(obj);
    }

    /// <summary>
    /// control if the buy is possible
    /// </summary>
    /// <returns>true if is a sucess </returns>
    private bool BuyIfPossible()
    {
        //select the component each time for update the selected tower, can't be apply save in a variable
        if (tower.Value != null)
        {
            int buycost = (int)tower.Value.GetComponent<Tower>().statDefault.buyCost;
            if (argent.Value - buycost >= 0)
            {
                argent.ApplyChange(-buycost);
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// control if the position is empty
    /// </summary>
    /// <param name="pos">The position.</param>
    /// <returns>true if empty</returns>
    private bool PlaceIsFree(Vector3 pos)
    {
        Collider[] hitCollidersBox = Physics.OverlapBox(pos, Vector3.one, Quaternion.identity, LayerMaskAtPosition);
        if (hitCollidersBox.Length == 0)
        {
            Collider[] hitCollidersSphere = Physics.OverlapSphere(pos, safeRadius, safeRadiusMask);
            if (hitCollidersSphere.Length == 0)
            {
                return true;
            }
        }
        return false;
    }
}
