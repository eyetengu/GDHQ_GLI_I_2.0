using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NPCFramework : MonoBehaviour
{
    //Enemy FSM
    private enum AIState
    {
        run, 
        idle, 
        death,
        hiding
    }

    [SerializeField]
    private AIState _currentState;


    //Other Fields
    [SerializeField]
    private Transform _startPoint, _endPoint;
    private NavMeshAgent _agent;

    //Audio
    [SerializeField] private AudioManager _audioManager;

    //Animation
    //[SerializeField] private AnimationManager _animationManager;
    //private PlayerAnimations _playerAnimations;


    void Start ()
    {
        _agent = GetComponent<NavMeshAgent>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _startPoint = GameObject.Find( "Start Point").GetComponent<Transform>();
    }

    void Update()
    {
        _agent.destination = _endPoint.position;
        //Debug.Log("Endpoint " + agent.destination);
        EnemyStateSelector();
    }

    //Enemy FSM
    //build out a complete FSM for idle, run and death
    //waypoints will be assigned to the various columns(hide zones)
    //when the enemies get close enough to the columns they will idle for a random amount of time
    //when idle time is up- enemies will run to the next column for cover- repeat for each enemy
    //idle- when reach cover
    //run- when timer reaches 0
    //death- when hit by bullet
    
    private void EnemyStateSelector()
    {        
        switch(_currentState)
        {
            case AIState.idle:
                Debug.Log("Idle");
                //_playerAnimations.Idling();
                this.GetComponent<NavMeshAgent>().speed = 0;
                break;

            case AIState.run:
                Debug.Log("running");
                //_playerAnimations.Running();
                this.GetComponent<NavMeshAgent>().speed = 4;
                break;

            case AIState.death:
                Debug.Log("Death");
                //Activate Function in the AnimationManager script
                    //_playerAnimations.Dying();
                //stop this character from moving
                    this.GetComponent<NavMeshAgent>().speed= 0;
                //begin countdown to make this character disappear from view
                    StartCoroutine(DeathDelay());
                break;

            case AIState.hiding:
                Debug.Log("Hiding");
                //_playerAnimations.Hiding();
                break;

            default:
                break;
        }
    }

    IEnumerator DeathDelay()                //remove dead player after XX seconds
    {
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndPoint")
        {
            Debug.Log("Enemy Entered Trigger");
            this.gameObject.SetActive(false);
            _audioManager.PlayAudioClip(2);
        }

        /*
        if (other.tag == "Bullet")
        {
            Debug.Log("I've had worse!");
            this.gameObject.SetActive(false);
            _audioManager.PlayAudioClip(3);
        }
        */
    }
}