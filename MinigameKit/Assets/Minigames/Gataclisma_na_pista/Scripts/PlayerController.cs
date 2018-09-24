using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GataclismaNaPista
{
    [System.Serializable]
    public class ScoreEvent : UnityEvent<ScoreType> { }

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
         * */
        public ScoreEvent onScoreChange;


        public float Score { get; private set; }
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
            sequence = GetComponent<ArrowSequence>();
            boxSize = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
        }

        void Start()
        {
            perfectDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2) / 8;
            greatDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2) / 4;
            goodDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2) / 2;
            almostDistance = (boxSize / 2 + ArrowSequence.arrowSize / 2);
            onScoreChange.AddListener(sequence.DestroyPeek);
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
                float distance = Mathf.Abs(this.transform.position.y - sequence.ArrowQueue.Peek().transform.position.y);
                ScoreType score;
                if (Input.GetAxisRaw(player.playerButtons.horizontal) == 1 && sequence.peekArrowScript.direction == Direction.right ||
                    Input.GetAxisRaw(player.playerButtons.vertical) == -1 && sequence.peekArrowScript.direction == Direction.down ||
                    Input.GetAxisRaw(player.playerButtons.horizontal) == -1 && sequence.peekArrowScript.direction == Direction.left ||
                    Input.GetAxisRaw(player.playerButtons.vertical) == 1 && sequence.peekArrowScript.direction == Direction.up)
                {
                    score = CalculateScore(distance);
                }
                    else { FailArrow(); Debug.Log("Wrong Arrow!"); score = ScoreType.wrongArrow; }
                onScoreChange.Invoke(score);
            }
        }

        private ScoreType CalculateScore(float distance)
        {
            float points;
            ScoreType score;
            if (distance < almostDistance)
            {
                if (distance < perfectDistance)
                {
                    Debug.Log("perfect!");
                    points = 5;
                    score = ScoreType.perfect;
                }
                else if (distance < greatDistance)
                {
                    Debug.Log("great!");
                    points = 3;
                    score = ScoreType.great;
                }
                else if (distance < goodDistance)
                {
                    Debug.Log("good!");
                    points = 2;
                    score = ScoreType.good;
                }
                else
                {
                    Debug.Log("almost!");
                    points = 1;
                    score = ScoreType.almost;
                }
                this.Score += points;
            }
            else
            {
                score = FailArrow();
            }
            return score;
        }

        private ScoreType FailArrow()
        {
            Debug.Log("fail!");
            float points = -2;
            if (this.Score + points < 1)
            {
                this.Score = 1;
            }
            else
            {
                this.Score += points;
            }
            return ScoreType.fail;
        }

        //define propriedades da variável "player"
        private void SetPlayer()
        {

        }


    }
}
