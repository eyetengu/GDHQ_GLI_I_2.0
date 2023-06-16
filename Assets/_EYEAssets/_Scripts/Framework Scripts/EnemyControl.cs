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
    private AudioManager _audioManager;

    //Waypoints
    [SerializeField] private List<Transform> _waypoints = new List<Transform>();
    [SerializeField] int _currentDestinationWaypoint, _randomWaypoint;
    [SerializeField] private float _randomWait;
    
    //Hiding
    [SerializeField] private bool _isHiding, _isRunning;
    public bool _isDead = false;

    [SerializeField] private UIControlScript _uiControl;


    private void Awake()
    {
        //Enemy
        _agent = GetComponent<NavMeshAgent>();
        _controlPlayerAnimation = GetComponent<ControlPlayerAnimation>();     
    }

    void Start()
    {
        //UIControl
        _uiControl = GameObject.Find("UIControl").GetComponent<UIControlScript>();

        if (_uiControl == null)
            Debug.Log("UIControl = NULL");

        //Control Player Animation
        if (_controlPlayerAnimation == null)
            Debug.LogError("ControlPlayerAnimation = NULL");

        //Waypoints
        if (_waypoints == null)
            Debug.LogError("Waypoints = NULL");        
        //Agent
        if(_agent == null)
            Debug.LogError("Agent = NULL");
        else 
        {
            _agent.destination = _waypoints[0].position;
            //SwitchAnimation(2);
            _isRunning = true;
            _isHiding = false;
        }
    }

    void Update()
    {
        if (_isRunning)
            SwitchAnimation(2);
        if (_isHiding)
            SwitchAnimation(3);

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
            case 0: //IDLE
                _controlPlayerAnimation.Idling();
                _agent.speed = 0.0f;
                    break;
            case 1: //WALK
                _controlPlayerAnimation.Walking();
                _agent.speed = 2.0f;
                    break;
            case 2: //RUN
                _controlPlayerAnimation.Running();
                _agent.speed = 4.1f;
                    break;
            case 3: //HIDE
                _controlPlayerAnimation.Hiding();
                _agent.speed = 0.0f;
                    break;
            case 4: //DIE
                _controlPlayerAnimation.Dying();
                _agent.speed = 0.0f;
                StartCoroutine(RemoveDeadBody());
                _isDead= true;
                _uiControl.UpdateScore(1);
                _audioManager.PlayAudioClip(6);
                    break;
            default:
                    break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EndPoint")
        {        
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator WaitAndRun()
    {
        //turn off ability to access coroutine
            _isHiding= true;
            _isRunning = false;

        //generate a random wait time for enemy to HIDE
            _randomWait = Random.Range(0.0f, 3.0f);
            yield return new WaitForSeconds( _randomWait);

        //Set animation RUN
            _isRunning = true;
        //find random waypoint between this and last count
            _randomWaypoint = Random.Range(_currentDestinationWaypoint, _waypoints.Count);
        //if check the waypoint and set to last waypoint if equal or greater than
            if(_randomWaypoint >= _waypoints.Count)
            _randomWaypoint = _waypoints.Count - 1;
        //set agent destination to random waypoint
            _agent.destination = _waypoints[_randomWaypoint].transform.position;
        //update current waypoint destination do that there is no backtracking
            _currentDestinationWaypoint = _randomWaypoint;
        //turn on ability to re-enter coroutine
            _isHiding= false;
    }

    IEnumerator RemoveDeadBody()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
