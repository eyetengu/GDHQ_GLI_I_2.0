using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PoolManager : MonoBehaviour
{    
    //turn this class into a singleton for easy accessibility
    private static PoolManager instance;
    //Property for the PoolManager
    public static PoolManager Instance
    { 
        get 
        {
            if (instance == null)
                Debug.Log("PoolManager is Null");
            
            return instance; 
        } 
    }

    [Header("ENEMY SETTINGS")]
    [SerializeField] private List<GameObject> _enemyPool;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;                                                                                    
    [SerializeField] private bool sendNextEnemy;

    [Header("END POINTS")]
    [SerializeField]
    private Transform _startPoint;

    [Header("AUDIO")]
    [Header("Audio Settings")]
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private AudioClip _enterEndpointClip;

    [SerializeField] private List<Transform> _waypoints;


    //MAIN FUNCTIONS
    void Awake()
    {
        instance= this;
    }

    private void Start()
    {
        //create 10 enemies and populate into enemy list
        _enemyPool = GenerateEnemies(10);
        sendNextEnemy = true;
    
        SpawnEnemy();
        //StartCoroutine(SpawnEnemyAtStartPoint());
    }

    //ENEMY INFORMATION
    //CREATE ENEMIES & ADD TO ENEMY LIST
    private List<GameObject> GenerateEnemies(int numberOfEnemies)
    {
        //Do This 10 Times
        for (int i = 0; i < numberOfEnemies; i++)
        {
            //Create an enemy from prefab
                GameObject enemy = Instantiate(_enemyPrefab);
            //Set the enemy as a child of the enemy container
                enemy.transform.parent = _enemyContainer.transform;
            //Disable the enemy gameobject
                enemy.SetActive(false);
            //Add enemy to the enemy pool
                _enemyPool.Add(enemy);

            //Give the enemy the waypoint map
                enemy.GetComponent<EnemyControl>().SetWaypoints(_waypoints);
        }         
        return _enemyPool;
    }   

    //SPAWN ENEMY AT START POINT
    public GameObject SpawnEnemy()
    {
        //loop through the enemy list
        //check for inactive enemy
        //if found- set to active and spawn

        foreach(GameObject enemy in _enemyPool)
        {
            Debug.Log("Prepping Enemy");
            if(enemy.activeInHierarchy == false)
            {
                enemy.SetActive(true);
                enemy.transform.position = _startPoint.position;

                StartCoroutine(SpawnEnemyAtStartPoint());

                _audioManager.PlayAudioClip(1);
                
                return enemy;
            }
        }
        return null;
    }

    //SPAWN ENEMY DELAY
    IEnumerator SpawnEnemyAtStartPoint()
    {
        Debug.Log("Enemy Spawned");
        yield return new WaitForSeconds(5); // (Random.Range(1, 2));
        
        SpawnEnemy();
    }
    
}