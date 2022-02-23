using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Zombie : MonoBehaviour
{
    NavMeshAgent nm;
    public Transform target;


    void Start()
    {
        nm = GetComponent<NavMeshAgent>();
        nm.SetDestination(target.position);
    }

    void Update()
    {
        
    }
}
