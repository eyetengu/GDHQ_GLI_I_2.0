using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AICharacterMover : MonoBehaviour
{
    //Create A Point-To-Point Waypoint Follower
    //store all waypoints
    //select a random waypoint
    //travel to selected waypoint

    //create a random waypoint selector

    //---------------------------------

    private enum AIStates{
        Walking,
        Jumping,
        Attacking,
        Death
    }

    [SerializeField]
    private AIStates _currentState;

    private Rigidbody rb;
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

    [SerializeField]
    private bool _isAttacking;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        

        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = true;
        GoToNextPoint();
    }

    void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
            _currentState = AIStates.Jumping;

        if(Keyboard.current.fKey.wasPressedThisFrame )
            _currentState = AIStates.Attacking;

        if(Keyboard.current.pKey.wasPressedThisFrame )
            _currentState = AIStates.Death;

        float distanceBetween = Vector3.Distance(_player.transform.position, transform.position);

        //Debug.Log(distanceBetween);

        if(distanceBetween < 5.0f)
            _currentState = AIStates.Attacking;
                        
        switch (_currentState)
        {
            case AIStates.Walking:
                agent.isStopped = false;
                Debug.Log("Walking");
                if (waypoints.Length == 0)                                  //null check our waypoints
                    return;

                if (RandomWaypoints == true)
                {
                    if (!agent.pathPending && agent.remainingDistance < .5)
                    {
                        randomPoint = Random.Range(0, waypoints.Length);
                        agent.destination = waypoints[randomPoint].position;
                    }
                    GoToRandomPoint();
                }                               //check for random waypoint toggle condition

                if (!agent.pathPending && agent.remainingDistance < 0.5f)     //check to see if player is close to point and if has a path pending
                {
                    if(_isRouteReversed == true)                            //route reversed logic
                        GoToPreviousPoint();
                    else 
                        GoToNextPoint();                                    //route forward logic
                }        
                break; 

            case AIStates.Jumping:
                Debug.Log("Jumping");
                rb.AddForce(0, 440, 0, ForceMode.Impulse);
                agent.isStopped= true;
                StartCoroutine(AIJumpMode());
                break; 
            
            case AIStates.Attacking:
                agent.isStopped = false;
                _isAttacking= true;
                Debug.Log("Attacking");
                StartCoroutine(AIAttackMode());
                break;
            
            case AIStates.Death:
                Debug.Log("Death");
                StartCoroutine(AIDeathState());
                break;
        }
    }

    IEnumerator AIAttackMode()
    { 
        yield return new WaitForSeconds(3f);
        _currentState = AIStates.Walking;
        _isAttacking= false;
    }
    IEnumerator AIJumpMode()
    {
        yield return new WaitForSeconds(2);
        _currentState = AIStates.Walking;
        agent.isStopped = false;
    }
    IEnumerator AIDeathState()
    {
        yield return new WaitForSeconds(3);
        _currentState = AIStates.Walking;
        agent.isStopped = false;
    }

    //pre-determined waypoints
    void GoToPreviousPoint()
    {
        destPoint --;                           //sets current destination point to zero when appropriate

        if (destPoint == 0)
            _isRouteReversed = false;

        GoToPoint();
    }

    void GoToNextPoint()
    {        
        destPoint++;                            //sets current destination point to zero when appropriate

        if (destPoint == waypoints.Length-1)
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
