using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float speed = 10;
    [SerializeField] float boost = 10;
    float fov;
    [SerializeField] float sensitivity = 10f;

    public LayerMask layerMask;
    [SerializeField] float toCloseDistance = 10;
    [SerializeField] float toFarDistance = 100;

    void Start()
    {

    }
    void Update()
    {
        bool toClose = false;
        bool toFar = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log(hit.distance);
            if (hit.distance <= toCloseDistance) toClose = true;
            if (hit.distance >= toFarDistance) toFar = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
            
        }


        #region move horizontal & vertical
        float sboost = (Input.GetKey(KeyCode.LeftShift) ? boost : 1);


        float hori = Input.GetAxis("Horizontal") * speed * Time.deltaTime * sboost;

        float verti = Input.GetAxis("Vertical") * speed * Time.deltaTime * sboost;


        transform.Translate(hori, 0, 0);
        transform.Translate(0, verti, 0);
        #endregion
        #region zoom
        if ((Input.GetAxis("Mouse ScrollWheel") > 0 && !toClose) || (Input.GetAxis("Mouse ScrollWheel") < 0 && !toFar))
        {
            fov = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            transform.Translate(0, 0, fov);
        }
        #endregion
    }
}
