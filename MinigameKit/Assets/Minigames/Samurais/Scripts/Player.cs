using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Samurais
{
    public class Player : PlayerInfo {
        const float DELTA_X = 11.5f;

        Animator _animator;
        Vector2 originalPosition;

        public Health health;
        public GameObject timingWarning;
        [HideInInspector] public bool locked;

        public Color baseColor;

	    public override void Start ()
        {
            base.Start();
            _animator = GetComponent<Animator>();
            originalPosition = transform.position;

            SpriteColorReplacement scr = GetComponent<SpriteColorReplacement>();
            scr.replacedColors = new List<SpriteColorReplacement.ReplacedColor>();
            scr.replacedColors.Add(new SpriteColorReplacement.ReplacedColor(baseColor, base.color));
            scr.Apply();
            //GetComponent<SpriteRenderer>().color = base.color;
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
            ClearVisualCues();
            health.TakeDamage();
            _animator.SetTrigger("Die");
        }

        public void Reset()
        {
            ClearVisualCues();
            locked = false;
            _animator.SetTrigger("Reset");
            transform.position = originalPosition;
        }

        void ClearVisualCues()
        {
            StopAllCoroutines();
            if (timingWarning) timingWarning.SetActive(false);
            transform.position = originalPosition;
        }
    }

}

