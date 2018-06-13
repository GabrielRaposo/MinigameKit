using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Samurais
{
    public class Player : MonoBehaviour {
        const float DELTA_X = 11.5f;

        Animator _animator;
        Vector2 originalPosition;

        public Health health;
        [HideInInspector] public bool locked;

	    void Start ()
        {
		    _animator = GetComponent<Animator>();
            originalPosition = transform.position;
        }

        public void Attack()
        {
            if (locked) return;
            if (transform.position.x < 0)
                transform.position += Vector3.right * DELTA_X;
            else
                transform.position += Vector3.left  * DELTA_X;
            _animator.SetTrigger("Attack");
        }

        public void Miss()
        {
            if (locked) return;
            locked = true;
            StartCoroutine(lockTimer());
            _animator.SetTrigger("Miss");
        }

        IEnumerator lockTimer()
        {
            yield return new WaitForSeconds(3);
            Reset();
        }

        public void Die()
        {
            StopAllCoroutines();
            health.TakeDamage();
            _animator.SetTrigger("Die");
        }

        public void Reset()
        {
            StopAllCoroutines();
            locked = false;
            _animator.SetTrigger("Reset");
            transform.position = originalPosition;
        }
    }

}

