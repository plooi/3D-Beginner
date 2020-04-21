﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    public Transform player;
    public float minDist = 0.00000005f;

    int m_CurrentWaypointIndex;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        minDist = 1;


        //Based off of this from untiy docs https://docs.unity3d.com/ScriptReference/Vector3.Dot.html
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toPlayer = player.position - transform.position;

        //Checks this first, then distance, so as to not do unnecessary calculation
        //doing these in the other order might be more efficient, not sure
 
        if(Vector3.Dot(forward, toPlayer) < 0)
        {
            float dist = Vector3.Distance(player.position, transform.position);
            if (dist < minDist)
            {
                
                navMeshAgent.SetDestination(player.position);
               
            }
        }
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); 
        }
    }
}