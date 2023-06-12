using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyControl : MonoBehaviour
{
    private NavMeshAgent _agent;
    ControlPlayerAnimation _controlPlayerAnimation;

    [SerializeField] List<AnimationClip> _animationClips;

    [SerializeField] private Transform _endPoint;

    [SerializeField] private int cycledCount = 0;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _controlPlayerAnimation = GetComponent<ControlPlayerAnimation>();
        _controlPlayerAnimation = GetComponent<ControlPlayerAnimation>();
        SwitchAnimation(cycledCount);
    }

    void Update()
    {
        _agent.destination = _endPoint.position;
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            cycledCount++;

            if(cycledCount > 4)
            {
                cycledCount = 0;
            }

            SwitchAnimation(cycledCount);
        }
    }

    void SwitchAnimation(int counter)
    {
        switch(counter)
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
}
