using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LaserFireScript : MonoBehaviour
{
    [SerializeField] private Transform _gunBarrel;
    [SerializeField] private GameObject _laserBulletPrefab;
    private void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Mouse pressed");
            Instantiate(_laserBulletPrefab, _gunBarrel.transform.position, Quaternion.identity);
        }
    }

}
