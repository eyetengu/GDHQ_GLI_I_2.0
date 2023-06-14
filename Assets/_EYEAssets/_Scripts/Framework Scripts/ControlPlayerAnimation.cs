using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlPlayerAnimation : MonoBehaviour
{
    private GameObject _femaleAgent;
    private Animator _femaleAnimation;
    [SerializeField] List<AnimationClip> _animationClips;

    private void Awake()
    {
        _femaleAgent = this.gameObject;
        _femaleAnimation = GetComponent<Animator>();
        
        if(_femaleAnimation == null)
        {
            Debug.Log("FemaleAnimation is null");
        }
    }
    
    public void Idling()
    {
        _femaleAnimation.SetFloat("Speed", 0);
        
        _femaleAnimation.SetBool("Hiding", false);
        _femaleAnimation.ResetTrigger("Death");
        
        Debug.Log("Idling");
    }
    
    public void Walking()
    {
        _femaleAnimation.SetFloat("Speed", 2);
        
        Debug.Log("Walk");
    }

    public void Running()
    {        
        _femaleAnimation.SetFloat("Speed", 5);        
        Debug.Log("Running");        
    }

    public void Hiding()
    {
        _femaleAnimation.SetBool("Hiding", true);        
        Debug.Log("Hiding");
    }

    public void Dying()
    {
        _femaleAnimation.SetTrigger("Death");        
        Debug.Log("Dying");
    }

}
