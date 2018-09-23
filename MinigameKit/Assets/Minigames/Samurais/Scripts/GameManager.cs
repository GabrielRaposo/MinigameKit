using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        AudioSource bgm; 

        enum GameState {Intro, Wait, Ready, Outro, End}
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
                    if (Input.GetButtonDown(leftSamurai.playerButtons.action)) {
                        OffTimingAttack(true);
                    }
                    if (Input.GetButtonDown(rightSamurai.playerButtons.action)) {
                        OffTimingAttack(false);
                    }
                    break;

                case GameState.Ready:
                    if (Input.GetButtonDown(leftSamurai.playerButtons.action)  && !leftSamurai.locked) {
                        DuelResults(true);
                    } else 
                    if (Input.GetButtonDown(rightSamurai.playerButtons.action) && !rightSamurai.locked) {
                        DuelResults(false);
                    }
                    //Depois lidar com empates, por enquanto prioriza o da esquerda
                    break;

                default:
                    break;
            }

            if (Input.GetKeyDown(KeyCode.O)) StartCoroutine(EndMinigame(true));
            if (Input.GetKeyDown(KeyCode.P)) StartCoroutine(EndMinigame(false));
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
                SetEndState(leftWins);
            
            loser.Die();
        }
        #endregion

        #region State Set
        void SetIntroState()
        {
            roundText.text = string.Empty;
            StartCoroutine(WaitForDoorToOpen());
            gameState = GameState.Intro;
        }

        void SetWaitState()
        {
            float time = Random.Range((float)minTimer, (float)maxTimer);
            bgm.Play();
            StartCoroutine(WaitingTimer(time));
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

        void SetEndState(bool leftWins)
        {
            StartCoroutine(EndMinigame(leftWins));
            gameState = GameState.End;
        }

        IEnumerator WaitingTimer(float time)
        {
            yield return new WaitForSeconds(time);
            SetReadyState();
        }

        IEnumerator WaitForDoorToOpen()
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

        IEnumerator EndMinigame(bool leftWins)
        {
            if (leftWins) {
                roundText.text = "Left wins!";
                PlayersManager.result = PlayersManager.Result.LeftWin;
            }
            else {
                roundText.text = "Right wins!";
                PlayersManager.result = PlayersManager.Result.RightWin;
            }

            yield return new WaitForSeconds(1);

            StartCoroutine(ModeManager.TransitionToMenu());
        }

        #endregion

    }
}
