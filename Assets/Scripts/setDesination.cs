using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class setDesination : MonoBehaviour
{
    NavMeshAgent myNavMeshAgent;
    public Transform slt;
    void Start()
    {


        myNavMeshAgent = GetComponent<NavMeshAgent>();

        myNavMeshAgent.SetDestination(slt.position);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetDestinationToMousePosition();
        }
    }

    void SetDestinationToMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            myNavMeshAgent.SetDestination(hit.point);
        }
    }
}
