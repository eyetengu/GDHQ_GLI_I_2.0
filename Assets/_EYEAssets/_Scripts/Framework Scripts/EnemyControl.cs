using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyControl : MonoBehaviour
{
    //Enemy Specifics
    private NavMeshAgent _agent;
    private ControlPlayerAnimation _controlPlayerAnimation;
    //Animations
    //[SerializeField] List<AnimationClip> _animationClips;
    //Waypoints
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] int _currentDestinationWaypoint, _randomWaypoint;
    //Hiding
    [SerializeField] private bool _isHiding;
    [SerializeField] private float _randomWait;


    void Start()
    {
        //Enemy
        _agent = GetComponent<NavMeshAgent>();
        _controlPlayerAnimation = GetComponent<ControlPlayerAnimation>();

        if (_controlPlayerAnimation == null)
            Debug.Log("No ControlPlayerAnimation Available");

        //Waypoints
        if (_waypoints == null)
            Debug.LogError("Waypoints Are NULL");        

        if(_agent == null)
            Debug.Log("No Agent Found");
        else 
        {
            _agent.destination = _waypoints[0].position;
            SwitchAnimation(2);
        }
    }

    void Update()
    {        
        //if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            //GoToNextPoint();!_agent.pathPending && _agent.remainingDistance < 0.5f

        if(_isHiding == false && !_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            StartCoroutine(WaitAndRun());
        }
    }

    public void SetWaypoints(List<Transform> points)
    {
        Debug.Log("Waypoints are being set");
        _waypoints = points;
    }

    private void GoToNextPoint()
    {

        //when arrived wait random time(1-3 sec)
        //choose my next destination (random current - count)
        //go to next destination
        //repeat
        //_agent.destination = _waypoints

        //_currentDestinationWaypoint
        
        _agent.destination = _waypoints[_waypoints.Count -1].transform.position;
    }

    void SwitchAnimation(int counter)
    {
        switch (counter)
        {
            case 0:
                _controlPlayerAnimation.Idling();
                _agent.speed = 0.0f;
                break;
            case 1:
                _controlPlayerAnimation.Walking();
                _agent.speed = 2.0f;
                break;
            case 2:
                _controlPlayerAnimation.Running();
                _agent.speed = 4.1f;
                break;
            case 3:
                _controlPlayerAnimation.Hiding();
                _agent.speed = 0.0f;
                break;
            case 4:
                _controlPlayerAnimation.Dying();
                _agent.speed = 0.0f;
                break;
            default:
                break;
        }
    }    

    IEnumerator WaitAndRun()
    {
        Debug.Log("Total Waypoints: " + _waypoints.Count);
        _isHiding = true;

        _randomWait = Random.Range(0.0f, 3.0f);

        yield return new WaitForSeconds( _randomWait);

        _randomWaypoint = Random.Range(_currentDestinationWaypoint, _waypoints.Count);

        if(_randomWaypoint >= _waypoints.Count)
            _randomWaypoint = _waypoints.Count - 1;

        _agent.destination = _waypoints[_randomWaypoint].transform.position;
        _currentDestinationWaypoint = _randomWaypoint;

        _isHiding= false;
    }

}
