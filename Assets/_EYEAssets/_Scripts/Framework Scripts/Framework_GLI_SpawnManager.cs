using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Framework_GLI_SpawnManager : MonoBehaviour
{
    [SerializeField]
    private AudioManager _audioManager;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _audioManager.PlayAudioClip(3);
            Debug.Log("Space Pressed");
        }
    }

    
}
