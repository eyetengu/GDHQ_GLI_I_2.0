using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    [SerializeField] private AudioManager _audioPlayer;
    [SerializeField] private PoolManager _poolManager;
    [SerializeField] private UIControlScript _uiControl;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            _audioPlayer.PlayAudioClip(3);
            Debug.Log("Game Over Trigger");
            _uiControl.UpdateEnemyScore(1);
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            //_audioPlayer.PlayAudioClip(3);
            //Debug.Log("Game Is Over Collision");
        }
    }
}
