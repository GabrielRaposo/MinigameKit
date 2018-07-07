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
        public GameObject sliceEffect;

        [Header("Gameplay Variables")]
        public int minTimer;
        public int maxTimer;

        KeyCode 
            leftAction = KeyCode.Z,
            rightAction = KeyCode.X;

        AudioSource bgm; 

        enum GameState {Intro, Wait, Ready, Outro}
        GameState gameState;
        #endregion

        #region Main Loop
        private void Start()
        {
            bgm = GetComponent<AudioSource>();

            SetIntroState();  
        }

        void Update ()
        {
            switch (gameState)
            {
                case GameState.Wait:
                    if (Input.GetKeyDown(leftAction)) {
                        OffTimingAttack(true);
                    }
                    if (Input.GetKeyDown(rightAction)) {
                        OffTimingAttack(false);
                    }
                    break;

                case GameState.Ready:
                    if (Input.GetKeyDown(leftAction)  && !leftSamurai.locked) {
                        DuelResults(true);
                    } else 
                    if (Input.GetKeyDown(rightAction) && !rightSamurai.locked) {
                        DuelResults(false);
                    }
                    //Depois lidar com empates, por enquanto prioriza o da esquerda
                    break;

                default:
                    break;
            }
        }

        void OffTimingAttack(bool leftAction)
        {
            if (leftAction)
                leftSamurai.Miss();
            else
                rightSamurai.Miss();
        }

        void DuelResults(bool leftWins)
        {
            sliceEffect.SetActive(true);

            Player winner, loser;
            if (leftWins)
            {
                winner = leftSamurai;
                loser = rightSamurai;
                sliceEffect.GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.up * 0);
            } else
            {
                winner = rightSamurai;
                loser = leftSamurai;
                sliceEffect.GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.up * 180);
            }

            winner.Attack();

            if (loser.health.value > 1) 
                SetOutroState();
            else 
                EndMatch(leftWins);
            
            loser.Die();
        }

        void EndMatch(bool leftWins) {
            if (leftWins) {
                roundText.text = "Left wins!";
                //MatchController.AddPoint(playerinfo[left<...
            } else {
                roundText.text = "Right wins!";
                //MatchController.AddPoint(playerinfo[right<...
            }

            gameState = GameState.Outro;
        }
        #endregion

        #region State Set
        void SetIntroState()
        {
            roundText.text = string.Empty;
            StartCoroutine(waitForDoorToOpen());
            gameState = GameState.Intro;
        }

        void SetWaitState()
        {
            float time = Random.Range((float)minTimer, (float)maxTimer);
            bgm.Play();
            StartCoroutine(waitingTimer(time));
            roundText.text = "Espere";
            gameState = GameState.Wait;
        }

        void SetReadyState()
        {
            bgm.Stop();
            roundText.text = "ATAQUE!";
            roundText.GetComponent<AudioSource>().Play();
            gameState = GameState.Ready;
        }

        void SetOutroState()
        {
            roundText.text = string.Empty;
            StartCoroutine(waitForDoorToClose());
            gameState = GameState.Outro;
        }

        IEnumerator waitingTimer(float time)
        {
            yield return new WaitForSeconds(time);
            SetReadyState();
        }

        IEnumerator waitForDoorToOpen()
        {
            transitionDoor.ToggleState();
            yield return new WaitUntil(() => transitionDoor.isOpen);
            SetWaitState();
        }

        IEnumerator waitForDoorToClose()
        {
            yield return new WaitForSeconds(1);

            transitionDoor.ToggleState();
            yield return new WaitUntil(() => !transitionDoor.isOpen);

            leftSamurai.Reset();
            rightSamurai.Reset();
            yield return new WaitForSeconds(1);
            SetIntroState();
        }
        #endregion

    }
}
