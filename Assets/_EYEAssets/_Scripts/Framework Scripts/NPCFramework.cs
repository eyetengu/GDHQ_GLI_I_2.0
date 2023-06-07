using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class NPCFramework : MonoBehaviour
{
    [SerializeField]
    private Transform endPoint;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        agent.destination = endPoint.position;
    }
}
