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
    private PoolManager _poolManager;

    //Animations
    //[SerializeField] List<AnimationClip> _animationClips;
    //Waypoints
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] int _currentDestinationWaypoint, _randomWaypoint;
    //Hiding
    [SerializeField] private bool _isHiding;
    [SerializeField] private float _randomWait;


    private void Awake()
    {
        //Enemy
        _agent = GetComponent<NavMeshAgent>();
        _controlPlayerAnimation = GetComponent<ControlPlayerAnimation>();        
    }

    void Start()
    {
        if (_controlPlayerAnimation == null)
            Debug.LogError("ControlPlayerAnimation = NULL");

        //Waypoints
        if (_waypoints == null)
            Debug.LogError("Waypoints = NULL");        

        if(_agent == null)
            Debug.LogError("Agent = NULL");
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
        //Debug.Log("Waypoints are being set");
        _waypoints = points;
    }    

    public void SwitchAnimation(int counter)
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EndPoint")
        {
            _poolManager.UpdateEnemyScore();            
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitAndRun()
    {
        //Debug.Log("Total Waypoints: " + _waypoints.Count);
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
