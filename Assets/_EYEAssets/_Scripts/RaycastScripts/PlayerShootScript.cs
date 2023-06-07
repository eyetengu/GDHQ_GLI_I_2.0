using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootScript : MonoBehaviour
{
    //cast raycast through center of reticle
    //instantiate a bullet hole at raycast hit

    [SerializeField]
    private GameObject _bulletHolePrefab;


    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                Instantiate(_bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
