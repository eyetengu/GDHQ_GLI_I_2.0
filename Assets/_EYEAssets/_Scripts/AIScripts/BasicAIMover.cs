using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class BasicAIMover : MonoBehaviour
{
    //Create A Point-To-Point Waypoint Follower
    //store all waypoints
    //create a random waypoint selector
    //select a random/non-random waypoint
    //travel to selected waypoint

    //---------------------------------

    [SerializeField]
    private GameObject _player;

    //fields
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Transform[] waypoints;

    //non-random points
    private int destPoint = 0;

    //reversed route
    [SerializeField]
    private bool _isRouteReversed;

    //Random Waypoint Search
    [SerializeField]
    private bool RandomWaypoints;
    private int randomPoint;   


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        GoToNextPoint();
    }

    void Update()
    {
        //null check our waypoints
        if (waypoints.Length == 0)                                  
            return;

        //check for random waypoint toggle condition
        if (RandomWaypoints == true)
        {
            if (!agent.pathPending && agent.remainingDistance < .5)
            {
                randomPoint = Random.Range(0, waypoints.Length);
                agent.destination = waypoints[randomPoint].position;
            }
            GoToRandomPoint();
        }

        //check to see if player is close to point and if has a path pending
        if (!agent.pathPending && agent.remainingDistance < 0.5f)     
        {
            //route reversed logic
            if (_isRouteReversed == true)                            
                GoToPreviousPoint();
            //route forward logic
            else
                GoToNextPoint();                                    
        }   
    }

    //pre-determined waypoints
    void GoToPreviousPoint()
    {
        destPoint--;                           //sets current destination point to zero when appropriate

        if (destPoint == 0)
            _isRouteReversed = false;

        GoToPoint();
    }

    void GoToNextPoint()
    {
        destPoint++;                            //sets current destination point to zero when appropriate

        if (destPoint == waypoints.Length - 1)
            _isRouteReversed = true;

        GoToPoint();
    }

    void GoToPoint()
    {
        agent.destination = waypoints[destPoint].position;  //moves agent to current destination point
    }

    //Random Waypoints
    void GoToRandomPoint()
    {
        agent.destination = waypoints[randomPoint].position;
    }
}
