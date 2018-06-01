using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Samurais {
    public class GameManager : MonoBehaviour {
        #region Variables
        [Header("References")]
        public Player leftSamurai;
        public Player rightSamurai;
        public TextMeshProUGUI roundText;
        public TransitionDoor transitionDoor;

        [Header("Gameplay Variables")]
        public int minTimer;
        public int maxTimer;

        KeyCode 
            leftAction = KeyCode.Z,
            rightAction = KeyCode.X;
	
        enum GameState { Wait, Ready, Transition}
        GameState gameState;
        #endregion

        private void Start()
        {
            setWaitState();  
        }

        void Update ()
        {
            //para testes
            if (Input.GetKeyDown(KeyCode.Space)) {
                transitionDoor.ToggleState();
            }

            switch (gameState)
            {
                case GameState.Wait:
                    break;

                case GameState.Ready:
                    if (Input.GetKeyDown(leftAction))
                    {
                        DuelResults(leftSamurai, rightSamurai);
                    }

                    if (Input.GetKeyDown(rightAction))
                    {
                        DuelResults(rightSamurai, leftSamurai);
                    }
                    //Depois lidar com empates, por enquanto prioriza o da esquerda
                    break;

                default:
                case GameState.Transition:
                    break;
            }
        }

        void DuelResults(Player winner, Player loser)
        {
            winner.Attack();
            loser.Die();

            setTransitionState();
        }

        #region States
        void setWaitState()
        {
            float time = Random.Range(minTimer, maxTimer);
            StartCoroutine(waitingTimer(time));
            roundText.text = "Wait";
            gameState = GameState.Wait;
        }

        IEnumerator waitingTimer(float time) {
            yield return new WaitForSeconds(time);
            setReadyState();
        }

        void setReadyState()
        {
            roundText.text = "ATTACK!!";
            gameState = GameState.Ready;
        }

        void setTransitionState()
        {
            roundText.text = string.Empty;
            gameState = GameState.Transition;
        }
        #endregion
    }
}
