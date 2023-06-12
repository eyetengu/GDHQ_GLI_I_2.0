using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControlPlayerAnimation : MonoBehaviour
{
    private GameObject _femaleAgent;
    private Animator _femaleAnimation;
    [SerializeField] List<AnimationClip> _animationClips;

    private void Start()
    {
        _femaleAgent = this.gameObject;
        _femaleAnimation = GetComponent<Animator>();
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
        //_femaleAnimation.Play("Running");
        //_femaleAnimation.SetTrigger("Hiding");

        Debug.Log("Running");
    }

    public void Hiding()
    {
        _femaleAnimation.SetBool("Hiding", true);
        //_femaleAnimation.SetFloat("Speed", 0);
        //_femaleAnimation.Play("CoverIdle");

        Debug.Log("Hiding");
    }

    public void Dying()
    {
        _femaleAnimation.SetTrigger("Death");
        //_femaleAnimation.SetFloat("Speed", 0);        
        //_femaleAnimation.Play("Death");
        //alt method for calling animation
        //_femaleAnimation.SetBool("Death", true);
        Debug.Log("Dying");
    }

}
