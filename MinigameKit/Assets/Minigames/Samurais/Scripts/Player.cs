using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Samurais
{
    public class Player : MonoBehaviour {
        const float DELTA_X = 10.5f;

	    Animator _animator;
        Vector2 originalPosition;

	    void Start ()
        {
		    _animator = GetComponent<Animator>();
            originalPosition = transform.position;
        }

        public void Attack()
        {
            _animator.SetTrigger("Attack");
            if (transform.position.x < 0)
                transform.position += Vector3.right * DELTA_X;
            else
                transform.position += Vector3.left  * DELTA_X;
        }

        public void Die()
        {
            _animator.SetTrigger("Die");
        }

        public void Reset()
        {
            _animator.SetTrigger("Reset");
            transform.position = originalPosition;
        }
    }

}

