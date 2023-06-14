using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShooter : MonoBehaviour
{
    private ControlPlayerAnimation _playerAnimation;
    private GameObject _enemy;
    private GameObject barrier;

    void Start()
    {
        _playerAnimation = GetComponent<ControlPlayerAnimation>();
        barrier = GameObject.Find("Barrier");
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "Enemy")
                {
                    Debug.Log("Enemy Hit");
                    _playerAnimation.Dying();
                }
                if(hit.collider.tag == "HidingSpot")
                {
                    Debug.Log("Barrier Down");
                    barrier = hit.collider.gameObject;
                    barrier.SetActive(false);
                    StartCoroutine(BarrierReup());
                }
            }
        }
    }

    IEnumerator BarrierReup()
    {
        yield return new WaitForSeconds(1);
        barrier.SetActive(true);
    }
}
