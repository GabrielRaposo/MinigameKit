using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Samurais
{
    public class Player : PlayerInfo {
        const float DELTA_X = 10.5f;

	    Animator _animator;
        Vector2 originalPosition;
        SpriteRenderer spriteRenderer;

	    public void Start ()
        {
            base.Start();
		    _animator = GetComponent<Animator>();
            originalPosition = transform.position;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.material.SetColor("Color_CCF45DDE", this.color);
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

