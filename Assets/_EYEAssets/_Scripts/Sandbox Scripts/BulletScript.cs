using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private int _cubeSpeed = 10;

    void OnEnable()
    {
        Invoke("Hide", 5);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _cubeSpeed);

    }

    void Hide()
    {
        this.gameObject.SetActive(false);
    }  

}
