using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
	private AudioSource source;

	void Start ()
	{
		source = GetComponent<AudioSource>();
	}

	public void PlaySound()
	{
		source.PlayOneShot(source.clip,1f);
	}
	
}
