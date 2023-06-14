using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShooter : MonoBehaviour
{
    //private EnemyControl _enemyControl;
    private ControlPlayerAnimation _playerAnimation;
    private EnemyControl _enemyControl;
    private AudioManager _audioManager;

    private GameObject _enemy;
    private GameObject barrier;

    private int _playerScore;


    private void Awake()
    {
        _playerAnimation = GetComponent<ControlPlayerAnimation>();        
        //_enemyControl = FindObjectOfType<EnemyControl>();
    }
    void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        //if (_enemyControl == null)
            //Debug.Log("EnemyControl = NULL");        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Firing Bullet");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            _audioManager.PlayAudioClip(3);

            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.tag);

                if(hit.collider.tag == "Enemy")
                {
                    _enemyControl = hit.transform.GetComponent<EnemyControl>();
                    Debug.Log("Enemy Hit");

                    //_enemy.GetComponent<ControlPlayerAnimation>().Dying();
                    _enemyControl.SwitchAnimation(4);
                    _playerScore++;
                    
                }
                if(hit.collider.tag == "Barrier")
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
