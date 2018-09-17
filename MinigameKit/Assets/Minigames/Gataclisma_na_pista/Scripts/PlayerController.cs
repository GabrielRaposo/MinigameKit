using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GataclismaNaPista
{
    [RequireComponent(typeof(ArrowSequence))]
    public class PlayerController : MonoBehaviour
    {
        /* Perfect > Great > Good > Almost */
        /*  1/4       1/4     1/4     1/4   */
        /*
         *      ____________________
         *     |       Almost       |
         *     |       Good         |
         *     |       Great        |
         *     |       Perfect      |
         *     |       Perfect      |
         *     |       Great        |
         *     |       Good         |
         *     |       Almost       |
         *      ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾
         *              
         * arrow = arrowCenter
         * cube = inputBoxCenter
         * 
         * 
         * */
        public float Score;// { get; private set; }
        private ArrowSequence sequence;
        private float boxSize;

        private float perfectDistance;
        private float greatDistance;
        private float goodDistance;
        private float almostDistance;

        //jogador da esquerda (player.side = false) ou direita (player.side = true)
        private PlayerInfo player;

        private void Awake()
        {
            player = transform.parent.GetComponent<PlayerInfo>();
            sequence = transform.GetComponent<ArrowSequence>();
            boxSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        }

        void Start()
        {
            perfectDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2) / 8;
            greatDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2) / 4;
            goodDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2) / 2;
            almostDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2);
            Debug.Log("Perfect = " + (this.transform.position.y + perfectDistance));
            Debug.Log("Great = " + (this.transform.position.y + greatDistance));
            Debug.Log("Good = " + (this.transform.position.y + goodDistance));
            Debug.Log("Almost = " + (this.transform.position.y + almostDistance));
        }

        void Update()
        {
            checkInputDirection();
        }

        private void checkInputDirection()
        {
            if (Input.GetButtonDown(player.playerButtons.horizontal) || Input.GetButtonDown(player.playerButtons.vertical))
            {
                if (Input.GetAxisRaw(player.playerButtons.horizontal) == 1 && sequence.peekArrowScript.direction == Direction.right ||
                   Input.GetAxisRaw(player.playerButtons.vertical) == -1 && sequence.peekArrowScript.direction == Direction.down ||
                   Input.GetAxisRaw(player.playerButtons.horizontal) == -1 && sequence.peekArrowScript.direction == Direction.left ||
                   Input.GetAxisRaw(player.playerButtons.vertical) == 1 && sequence.peekArrowScript.direction == Direction.up)
                    CalculateScore();
                else { FailArrow(); Debug.Log("Wrong Arrow!"); }
            }
        }

        private void CalculateScore()
        {
            float distance = Mathf.Abs(this.transform.position.y - sequence.ArrowQueue.Peek().transform.position.y);
            if (distance < almostDistance)
            {
                sequence.peekArrowScript.animator.Play("ArrowExplode");
                if (distance < perfectDistance)
                {
                    Debug.Log("perfect!");
                    this.Score += 5;
                }
                else if (distance < greatDistance)
                {
                    Debug.Log("great!");
                    this.Score += 3;
                }
                else if (distance < goodDistance)
                {
                    Debug.Log("good!");
                    this.Score += 2;
                }
                else
                {
                    Debug.Log("almost!");
                    this.Score += 1;
                }
            }
            else
            {
                FailArrow();
            }
        }

        private void FailArrow()
        {
            Debug.Log("fail!");
            this.Score -= 2;
            if (this.Score < 1) this.Score = 1;
        }

        //define propriedades da variável "player"
        private void SetPlayer()
        {

        }


    }
}
