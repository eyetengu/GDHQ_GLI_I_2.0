using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] private AudioClip _clip01;         //enemy spawned
    [SerializeField] private AudioClip _clip02;         //enemy entered end zone
    [SerializeField] private AudioClip _clip03;         //game over
    [SerializeField] private AudioClip _clip04;         //enemy hit

    void Start()
    {
        m_AudioSource= GetComponent<AudioSource>();
    }

    public void PlayAudioClip(int trackNumber)
    {
        switch(trackNumber)
        {
            case 1:                
                m_AudioSource.PlayOneShot(_clip01);
                break;
            case 2:
                m_AudioSource.PlayOneShot(_clip02);
                break; 
            case 3:
                m_AudioSource.PlayOneShot(_clip03);
                break;
            default:
                break;
        }
    }
}
