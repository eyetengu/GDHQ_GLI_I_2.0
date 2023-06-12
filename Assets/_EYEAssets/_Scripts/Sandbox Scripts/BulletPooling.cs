using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletPooling : MonoBehaviour
{
    private static BulletPooling instance;

    public static BulletPooling Instance
    {
        get
        {
            if(instance == null)
                Debug.Log("Bulletpooling instance is NULL");

            return instance;            
        }
    }

    //bullet control fields
    [SerializeField] private List<GameObject> _bulletPool;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _bulletContainer;

    //gun transform
    [SerializeField] private Transform _gunBarrelEnd;

    //fire delay fields
    [SerializeField] private bool _canFire;
    [SerializeField] private float _fireRate = 1;
    [SerializeField] private float _whenCanFireAgain;

    //Methods
    private void Awake()
    {
        //declare this instance(the only one) of this script occurrence
        instance= this;
    }

    private void Start()
    {
        //create 10 bullets and populate into list
        _bulletPool = GenerateBullets(10);
    }

    private List<GameObject> GenerateBullets(int count)     //this is how the list is created
    {
        for (int i = 0; i < count; i++)
        {
            //instantiate a bullet
            GameObject bullet = Instantiate(_bulletPrefab);
            //set bullet container as parent. place bullet inside
            bullet.transform.parent = _bulletContainer.transform; 
            //disable the bullet
            bullet.SetActive(false);
            //Add() the bullet to the List<>
            _bulletPool.Add(bullet);
        }
        //once the for loop is complete we will return the populated list
        return _bulletPool;
    }

    public GameObject RequestBullet()
    {
        //loop through the bullet list
        foreach(GameObject bullet in _bulletPool)
        {
            //check to see if we have any available bullets
            if(bullet.activeInHierarchy == false)
            {
                bullet.transform.position = _gunBarrelEnd.position;
                //if we do then fire that bullet
                bullet.SetActive(true);
                //return the bullet to the calling method
                return bullet;
            }
        }

        //If we do NOT have any available bullets
        //create a new bullet
        GameObject newBullet= Instantiate(_bulletPrefab, _gunBarrelEnd.transform.position, Quaternion.identity);
        //set new bullet as a child of the bullet container
        newBullet.transform.parent = _bulletContainer.transform;
        //Fire the new bullet
        newBullet.SetActive(true);
        //add this new bullet to the bullet pool
        _bulletPool.Add(newBullet);

        //return the new bullet to the calling method
        return newBullet;
    }

    void Update()
    {
        //use NUIS mouse input
        //check shooting conditions
        if (Mouse.current.leftButton.wasPressedThisFrame && _canFire)
        {
            //do not allow more bullets until cooldown has been reached
            _canFire = false;
            //minimize spam firing
            _whenCanFireAgain = Time.time + _fireRate;
            //fire bullet
            RequestBullet();
            
            //Instantiate(_bulletPrefab, _gunBarrelEnd.transform.position, Quaternion.identity);

        }
        //if duration is exceeded
        if (Time.time > _whenCanFireAgain)
        {
            //allow for next bullet to be fired
            _canFire = true;
        }
    }
}
