using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterAnimation : MonoBehaviour {

    Animator _animator;

	void Awake ()
    {
        _animator = GetComponent<Animator>();
    } 
	
    public void SetObserver(MonoBehaviour script)
    {
        
    }

	public void DisableObject()
    {
        _animator.enabled = false;
        gameObject.SetActive(false);
    }
}
