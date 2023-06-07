using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Click It Or Die
    //requires user input(mouse click)
    //fire raycast from the main camera or the mouse position
    //access the object we hit

    //Multiple Objects
    //when clicked: cubes(random color), sphere(no effect), capsule(black)

    //Click To Instantiate
    //when floor(ground plane) is clicked a cube will instantiate at the point that floor was clicked on.
    //variable for the prefab to instantiate
    //if left click -> raycast(origin = mousePos)
    //--hitInfo (to detect the floor)
    //--instantiate sphere at hitPoint

    //Layer Masks

//--------------------

    //Variable Sets
    [SerializeField]
    private GameObject _sphere;

    private Rigidbody _rb;
    [SerializeField]
    private GameObject _parachute;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        //ColorChangerLegacy();
        //ColorChangerNUIS();

        //MultipleObjectColorChange();

        //ClickToInstantiate();
        //ClickToInstantiate2();

        //LayerMask();

    }

    private void FixedUpdate()
    {
        DirectionalRays();        
    }

    //Directional Rays
    void DirectionalRays()
    {
        Debug.DrawLine(transform.position, Vector3.down * 1f, Color.blue);
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, Vector3.down, out hitInfo, 3f))
        {
            if (hitInfo.transform.tag == "Ground")
            {
                Debug.Log("Hit Ground");
                
                Debug.DrawLine(transform.position, Vector3.down * 3f, Color.red);
                _parachute.SetActive(true);
                _rb.drag = 15;
                //_rb.isKinematic = true;
                _rb.useGravity = false;
            }
            
        }
    }

    //Layer Masks
    void LayerMask()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6 | 1<< 7))
            {
                MeshRenderer renderer = hit.transform.GetComponent<MeshRenderer>();
                if(renderer != null)
                {
                    renderer.material.color = Color.red ;
                    Debug.Log("hit");
                }
            }
        }
    }

    //Click To Instantiate
    void ClickToInstantiate2() //GDHQ Version
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100))
            {
                Instantiate(_sphere, hit.point, Quaternion.identity  );
                Debug.Log("Value" + hit.transform.name);                
            }
        }
    }

    void ClickToInstantiate()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Ready");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                Instantiate (_sphere, hit.point, Quaternion.identity);
                if(hit.transform.tag == "Ground")
                {                                        
                    Debug.Log("Ground Hit" + Input.mousePosition);
                }
            }
        }
    }

    //Multiple Objects
    void MultipleObjectColorChange()
    {
        //if cube -> random color
        //if capsule -> black color
        //sphere -> no effect
        if(Input.GetMouseButton(0))
        {
            Debug.Log("Mouse");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100))
            {
                MeshRenderer renderer = hit.transform.GetComponent<MeshRenderer>();

                Debug.Log(hit.transform.tag.ToString());

                if(renderer != null)
                {
                    if(hit.transform.tag == "Cube")
                    {
                        renderer.material.color = Random.ColorHSV();
                    }

                    if(hit.transform.tag == "Capsule")
                    {
                        renderer.material.color = Color.black;
                    }
                }
            }
        }
    }

    //ClickItOrDie
    void ColorChangerLegacy()
    {
        if(Input.GetMouseButton(0))//Legacy Input System
        {
            Debug.Log("How");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100))
            {
                Debug.Log(hit.transform.name);
                MeshRenderer renderer = hit.transform.GetComponent<MeshRenderer>();
                renderer.material.color = Random.ColorHSV();
            }
        }
    }

    void ColorChangerNUIS()
        {
            //GDHQ Version(NUIS)
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Debug.Log("Left Clicked");

                Vector2 mousePos = Mouse.current.position.ReadValue();
                //define the starting point of the ray.

                Ray rayOrigin = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hitInfo;
                if (Physics.Raycast(rayOrigin, out hitInfo))
                {

                    MeshRenderer hitObject = hitInfo.collider.GetComponent<MeshRenderer>();

                    if(hitObject != null)
                    {
                        Debug.Log("Hit: " + hitInfo.transform.name);
                        hitObject.material.color = Random.ColorHSV();

                    }
                }

        }
    }

}
