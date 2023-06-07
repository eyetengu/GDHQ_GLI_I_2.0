using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlayer : MonoBehaviour
{
    //calculate the distance and direction
    //move towards target

    private Vector3 _targetDestination;

    void Update()
    {
        float distance = Vector3.Distance(_targetDestination, transform.position);

        if (distance > 1.0f)
        {
            //direction = destination - source
            var direction = _targetDestination- transform.position;
            direction.Normalize();

            //move towards destination
            transform.Translate(direction * 2.0f * Time.deltaTime);
        }
    }

    public void UpdateDestination(Vector3 pos)
    {
        //lock y to a fixed height
        pos.y = 0.5f;
        _targetDestination = pos;
    }
}
