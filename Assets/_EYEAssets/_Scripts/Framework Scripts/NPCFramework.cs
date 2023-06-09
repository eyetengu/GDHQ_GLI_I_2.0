using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class NPCFramework : MonoBehaviour
{
    //Enemy FSM
    private enum _enemyState
    {
        run, 
        idle, 
        death
    }

    [SerializeField]
    private string _currentState;

    //Other Fields
    [SerializeField]
    private Transform endPoint;
    private NavMeshAgent agent;

    //Audio
    [SerializeField] private AudioManager _audioManager;


    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        agent.destination = endPoint.position;
        //Debug.Log("Endpoint " + agent.destination);        
    }

    //Enemy FSM
    private void EnemyStateSelector()
    {
        string enemyState = _currentState;
        switch(enemyState)
        {
            case "run":
                break;
            case "idle":
                break;
            case "death":
                break;
            default:
                break;
        }

    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndPoint")
        {
            Debug.Log("Enemy Entered Trigger");
            this.gameObject.SetActive(false);
            _audioManager.PlayAudioClip(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "EndPoint")
        {
            Debug.Log("Enemy Entered Collision");

            //this.gameObject.SetActive(false);
            //_audioManager.PlayAudioClip(2);
        }
    }
}
