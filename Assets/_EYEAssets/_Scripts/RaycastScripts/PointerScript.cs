using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointerScript : MonoBehaviour
{    
    private ClickPlayer _player;
    private Vector3 _targetDestination;


    private void Start()
    {
        _player = FindObjectOfType<ClickPlayer>();
        
        if (_player == null)
            Debug.LogError("Failed To Locate Player");
    }

        
    void Update()
    {
        
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.name == "Floor")
                {
                    //update position to move towards
                    _player.UpdateDestination(hit.point);
                }
            }
        }       
    }
}