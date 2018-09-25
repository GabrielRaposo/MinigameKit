using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GataclismaNaPista
{
    public class CatAnimation : MonoBehaviour
    {

        private Animator animator;
        private PlayerInfo player;
        private int BPM;

        /*[System.NonSerialized]
        public Color color;*/

        private void Awake()
        {
            animator = transform.GetComponent<Animator>();
            player = transform.parent.GetComponent<PlayerInfo>();
            BPM = GameObject.FindObjectOfType<GameManager>().BPM;
        }

        public void Start()
        {
            //GetComponent<SpriteRenderer>().color = this.color;
        }

        public void playAnimation(ScoreType score)
        {
            if (score != ScoreType.wrongArrow && score != ScoreType.fail)
            {
                if(Input.GetAxisRaw(player.playerButtons.horizontal) == 1)
                {
                    animator.Play("Right");
                }
                else if(Input.GetAxisRaw(player.playerButtons.vertical) == -1)
                {
                    animator.Play("Down");
                }
                else if(Input.GetAxisRaw(player.playerButtons.horizontal) == -1)
                {
                    animator.Play("Left");
                }
                else if(Input.GetAxisRaw(player.playerButtons.vertical) == 1)
                {
                    animator.Play("Up");
                }
            }
            else
            {
                animator.Play("Fail");
            }
            StopAllCoroutines();
            StartCoroutine(AnimationDelay());
        }

        IEnumerator AnimationDelay()
        {
            yield return new WaitForSeconds(60f / BPM * 1.5f /* esse 1.5f é gambiarra; deveria ter um multiplicador da duration aqui */);
            animator.Play("Default");
        }
    }
}
