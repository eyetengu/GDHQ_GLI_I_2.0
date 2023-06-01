using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Debug.Log("How");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);
                MeshRenderer renderer = hit.transform.GetComponent<MeshRenderer>();
                renderer.material.color = Color.blue;
            }

        }
    }
}
