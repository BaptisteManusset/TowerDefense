using NaughtyAttributes;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] float speed = 10;
    [SerializeField] float boost = 10;
    float fov;
    [SerializeField] float sensitivity = 10f;

    [SerializeField] LayerMask layerMask;
    [SerializeField] [ReadOnly] float toCloseDistance = 10;
    [SerializeField] [ReadOnly] float toFarDistance = 100;

    void FixedUpdate()
    {
        bool toClose = false;
        bool toFar = false;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.distance <= toCloseDistance) toClose = true;
            if (hit.distance >= toFarDistance) toFar = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }


        #region move horizontal & vertical
        if (Time.timeScale != 0)
        {
            float sboost = (Input.GetKey(KeyCode.LeftShift) ? boost : 1);


            float hori = Input.GetAxis("Horizontal") * speed * Time.deltaTime * sboost / Time.timeScale;

            float verti = Input.GetAxis("Vertical") * speed * Time.deltaTime * sboost / Time.timeScale;


            //transform.Translate(hori, 0, 0);
            transform.position += new Vector3(hori, 0, 0);
            transform.position += new Vector3(0, 0, verti);
        }
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
