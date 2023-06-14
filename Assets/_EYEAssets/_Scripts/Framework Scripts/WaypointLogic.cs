using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private EnemyControl _enemyControl;

    [SerializeField] private Transform[] _waypoints;

    [SerializeField] private int destPoint = 0;

    [SerializeField] private Vector3 _nextWaypoint;

    private void Start()
    {        
        _agent.autoBraking = true;
        GoToFirstPoint();
    }

    private void Update()
    {
        //enemy requests new hiding spot
        //waypoints calculate a new random number for hiding spot and stores waypoint(V3) in newHidingSpot
        //if newHidingSpot > currentHidingSpot
        //-send enemy the newHidingSpot(V3)
        //-then update currentHidingSpot == newHidingSpot




    }

    void GoToFirstPoint ()
    { 
        _agent.destination = _waypoints[destPoint].position;
        //Debug.Log(destPoint.ToString());
    }

    public Vector3 GoToNextPoint(Vector3 position)
    {
        destPoint++;
        int waypointLength = _waypoints.Length;
        if (_waypoints.Length == 0 || destPoint >= _waypoints.Length - 1)
            return _waypoints[waypointLength - 1].position;
        else
        {
            var hidingPosition = _waypoints[destPoint].position;
           return hidingPosition;

        }
        
    }

    public Vector3 MoveToWaypoint (Vector3 position)
    {
        _agent.destination = _waypoints[destPoint].position;
        var destinationUpdated = _agent.destination;
        return destinationUpdated;

    }

    //enemy logic to choose a waypoint, run to it, hide behind it
    //if enemy has NOT reached end point
    //-choose, run, hide
    //else if enemy HAS reached end point
    //-nothing more to be done.
    //if enemy reaches waypoint
    //-enemy score increases
    //if (enemy score > (enemy.Count/2))
    //-Game Over- Enemy Wins
    //if(all enemies dead or inactive
    //-Game Over- Player Wins

    public Vector3 ReturnNextWaypoint()
    {
        Vector3 _waypointVector = _waypoints[destPoint].position;
        return _waypointVector;
    }
}
