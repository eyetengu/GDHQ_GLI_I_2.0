using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{    
    //turn this class into a singleton for easy accessibility
    private static PoolManager instance;
    public static PoolManager Instance
    { 
        get 
        {
            if (instance == null)
                Debug.Log("PoolManager is Null");
            
            return instance; 
        } 
    }

    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField] 
    private GameObject _enemyPrefab;
    [SerializeField] 
    private List<GameObject> _enemyPool;

    [SerializeField]
    private Transform _startPoint;
    [SerializeField]
    private bool sendNextEnemy;

    [SerializeField]
    //private AudioSource _audioPlayer;
    private AudioManager _audioManager;
    [SerializeField]
    private AudioClip _clipEnter;

    void Awake()
    {
        instance= this;
    }

    private void Start()
    {
        sendNextEnemy = true;
        _enemyPool = GenerateEnemies(10);

        SpawnEnemy();
        //StartCoroutine(SpawnEnemyAtStartPoint());
    }

    private void Update()
    {
        //if(sendNextEnemy == true)
        //{
        //    StartCoroutine(SpawnEnemyAtStartPoint());
        //}
    }

    //pregenerate a list of enemies using the enemy prefab
    List<GameObject> GenerateEnemies(int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            enemy.transform.parent = _enemyContainer.transform;
            enemy.SetActive(false);
            _enemyPool.Add(enemy);
        }         
        return _enemyPool;
    }   

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
                
                //_audioPlayer.PlayOneShot(_clipEnter);
                return enemy;
            }
        }
        return null;
    }

    IEnumerator SpawnEnemyAtStartPoint()
    {
        Debug.Log("Enemy Spawned");
        yield return new WaitForSeconds(5); // (Random.Range(1, 2));
        //sendNextEnemy = true;
        SpawnEnemy();
    }
}
