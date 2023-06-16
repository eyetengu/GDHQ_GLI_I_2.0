using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource m_AudioSource;

    [SerializeField] private AudioClip _enemySpawned;
    [SerializeField] private AudioClip _enemyScores;
    [SerializeField] private AudioClip _youLose;
    [SerializeField] private AudioClip _youWin;
    [SerializeField] private AudioClip _shotFired;
    [SerializeField] private AudioClip _enemyDies;
    [SerializeField] private AudioClip _barrierDestroyed;
    [SerializeField] private AudioClip _barrierStarted;
    [SerializeField] private AudioClip _barrelExplosion;

    void Start()
    {
        m_AudioSource= GetComponent<AudioSource>();
    }

    public void PlayAudioClip(int trackNumber)
    {
        switch(trackNumber)
        {
            case 1:                
                m_AudioSource.PlayOneShot(_enemySpawned);
                    break;
            case 2:
                m_AudioSource.PlayOneShot(_enemyScores);
                    break; 
            case 3:
                m_AudioSource.PlayOneShot(_youLose);
                    break;
            case 4:
                m_AudioSource.PlayOneShot(_youWin);
                    break;
            case 5:
                m_AudioSource.PlayOneShot(_shotFired);
                    break;
            case 6:
                m_AudioSource.PlayOneShot(_enemyDies);
                    break;
            case 7:
                m_AudioSource.PlayOneShot(_barrierDestroyed);
                    break;
            case 8:
                m_AudioSource.PlayOneShot(_barrierStarted);
                    break;
            case 9:
                m_AudioSource.PlayOneShot(_barrelExplosion);
                    break;  
            default:
                    break;
        }
    }
}
