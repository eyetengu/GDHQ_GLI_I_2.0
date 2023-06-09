using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    [SerializeField] private AudioManager _audioPlayer;

    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            //_audioPlayer.PlayAudioClip(3);
            //Debug.Log("Game Over Trigger");
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
