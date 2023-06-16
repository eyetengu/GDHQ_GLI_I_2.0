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
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Start()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        Debug.Log("Firing Bullet");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        _audioManager.PlayAudioClip(5);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6 | 1 << 7 | 1 << 8))
        {
            Debug.Log("Hit: " + hit.collider.tag);

            if(hit.collider.tag == "Enemy")
            {
                var enemyCollider = hit.collider.gameObject.GetComponent<CapsuleCollider>();
                enemyCollider.enabled = false;
                _enemyControl = hit.transform.GetComponent<EnemyControl>();
                Debug.Log("Enemy Hit");

                //_enemy.GetComponent<ControlPlayerAnimation>().Dying();
                _enemyControl.SwitchAnimation(4);
                _playerScore++;
                Debug.Log("Player Score: " + _playerScore);
                _audioManager.PlayAudioClip(6);
                    
            }
            if(hit.collider.tag == "Barrier")
            {
                Debug.Log("Barrier Down");
                barrier = hit.collider.gameObject;
                barrier.SetActive(false);
                StartCoroutine(BarrierReup());
                _audioManager.PlayAudioClip(7);
            }

            if(hit.collider.tag == "Barrel")
            {
                Debug.Log("HitBarrel");
                var explodingBarrelScript = hit.collider.GetComponent<ExplosiveBarrelScript>();
                hit.collider.enabled= false;
                explodingBarrelScript.TriggerBarrelExplosion();
                _audioManager.PlayAudioClip(9);                
            }
        }
    }

    IEnumerator BarrierReup()
    {
        yield return new WaitForSeconds(1);
        barrier.SetActive(true);
        _audioManager.PlayAudioClip(8);
    }
}
