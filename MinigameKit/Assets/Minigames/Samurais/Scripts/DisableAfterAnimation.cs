using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Samurais
{
    public class DisableAfterAnimation : MonoBehaviour {

	    void OnEnable () {
            Animator animator = GetComponent<Animator>();
            if(animator)
                StartCoroutine(TurnOff(animator.GetCurrentAnimatorStateInfo(0).length - .01f, animator));
        }

        IEnumerator TurnOff(float time, Animator animator)
        {
            yield return new WaitForSeconds(time);
            animator.SetTrigger("reset");
            gameObject.SetActive(false);
        }

    }
}
