using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFireBehavior : MonoBehaviour
{
    [SerializeField] private int _firingSpeed = 50;
    

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _firingSpeed);
    }
}
