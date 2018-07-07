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
        public GameObject timingWarning;
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
            StartCoroutine(tremble());
            if (timingWarning) timingWarning.SetActive(true);
            _animator.SetTrigger("Miss");
        }

        IEnumerator lockTimer()
        {
            yield return new WaitForSeconds(3);
            Reset();
        }

        IEnumerator tremble()
        {
            const float MAX_TREMBLE = .4f;
            float trembleValue = MAX_TREMBLE;
            for (int i = 0; i < 10; i++)
            {
                transform.position += Vector3.right * trembleValue * (i%2==0 ? 1 : -1);
                trembleValue -= (MAX_TREMBLE / 10);
                yield return new WaitForSeconds(.02f);
            }
            transform.position = originalPosition;
        }

        public void Die()
        {
            ClearVisualQueues();
            health.TakeDamage();
            _animator.SetTrigger("Die");
        }

        public void Reset()
        {
            ClearVisualQueues();
            locked = false;
            _animator.SetTrigger("Reset");
            transform.position = originalPosition;
        }

        void ClearVisualQueues()
        {
            StopAllCoroutines();
            if (timingWarning) timingWarning.SetActive(false);
            transform.position = originalPosition;
        }
    }

}

