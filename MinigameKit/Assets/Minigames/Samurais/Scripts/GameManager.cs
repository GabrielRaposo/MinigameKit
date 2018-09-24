using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

namespace Samurais {
    public class GameManager : MonoBehaviour {

        #region Variables
        [Header("References")]
        public Player leftSamurai;
        public Player rightSamurai;
        public TextMeshProUGUI actionDisplay;
        public TextMeshProUGUI roundDisplay;
        public TransitionDoor transitionDoor;
        public GameObject sliceEffect;

        [Header("Gameplay Variables")]
        public int minTimer;
        public int maxTimer;

        AudioSource bgm;
        int roundIndex;
        Coroutine timeOutCoroutine;

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
                        DuelResults(1);
                    } else 
                    if (Input.GetButtonDown(rightSamurai.playerButtons.action) && !rightSamurai.locked) {
                        DuelResults(2);
                    }
                    //Depois lidar com empates, por enquanto prioriza o da esquerda
                    break;

                default:
                    break;
            }

            if (Input.GetKeyDown(KeyCode.I)) StartCoroutine(EndMinigame(0));
            if (Input.GetKeyDown(KeyCode.O)) StartCoroutine(EndMinigame(1));
            if (Input.GetKeyDown(KeyCode.P)) StartCoroutine(EndMinigame(2));
        }

        void OffTimingAttack(bool leftAction)
        {
            if (leftAction)
                leftSamurai.Miss();
            else
                rightSamurai.Miss();
        }

        void DuelResults(int result)
        {
            StopCoroutine(timeOutCoroutine);

            if (result == 1) {
                sliceEffect.SetActive(true);
                leftSamurai.Attack();
                if (rightSamurai.health.value > 1)
                    SetOutroState();
                else
                    SetEndState();
                rightSamurai.Die();
            } else
            if (result == 2) {
                sliceEffect.SetActive(true);
                rightSamurai.Attack();
                if (leftSamurai.health.value > 1)
                    SetOutroState();
                else
                    SetEndState();
                leftSamurai.Die();
            } else
            {
                if (leftSamurai.health.value < 2 || rightSamurai.health.value < 2) {
                    SetEndState();
                } else {
                    SetOutroState();
                }
                leftSamurai.Die();
                rightSamurai.Die();
            }
        }
        #endregion

        #region State Set
        void SetIntroState()
        {
            actionDisplay.text = string.Empty;
            StartCoroutine(WaitForDoorToOpen());
            gameState = GameState.Intro;
        }

        void SetWaitState()
        {
            float time = Random.Range((float)minTimer, (float)maxTimer);
            bgm.Play();
            StartCoroutine(WaitingTimer(time));
            actionDisplay.text = "Espere";
            gameState = GameState.Wait;
        }

        void SetReadyState()
        {
            bgm.Stop();
            actionDisplay.text = "ATAQUE!";
            actionDisplay.GetComponent<AudioSource>().Play();
            timeOutCoroutine = StartCoroutine(TimeOutTimer());
            gameState = GameState.Ready;
        }

        void SetOutroState()
        {
            actionDisplay.text = string.Empty;
            StartCoroutine(WaitForDoorToClose());
            gameState = GameState.Outro;
        }

        void SetEndState()
        {

            if (leftSamurai.health.value > rightSamurai.health.value) {
                StartCoroutine(EndMinigame(1));
            } else
            if (rightSamurai.health.value > leftSamurai.health.value) {
                StartCoroutine(EndMinigame(2));
            } else {
                StartCoroutine(EndMinigame(0));
            }
            gameState = GameState.End;
        }

        IEnumerator WaitingTimer(float time)
        {
            yield return new WaitForSeconds(time);
            SetReadyState();
        }

        IEnumerator WaitForDoorToOpen()
        {
            //change round display
            yield return ShowRound();

            transitionDoor.ToggleState();
            yield return new WaitUntil(() => transitionDoor.isOpen);
            SetWaitState();
        }

        IEnumerator ShowRound()
        {
            roundDisplay.enabled = false;
            yield return new WaitForSeconds(.5f);

            roundIndex++;
            roundDisplay.transform.localScale = Vector3.one * 3;
            roundDisplay.text = "Round " + roundIndex;
            roundDisplay.enabled = true;
            roundDisplay.transform.DOScale(1, .1f);
            roundDisplay.GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1);
            roundDisplay.enabled = false;
        }

        IEnumerator WaitForDoorToClose()
        {
            yield return new WaitForSeconds(2.5f);

            transitionDoor.ToggleState();
            yield return new WaitUntil(() => !transitionDoor.isOpen);

            leftSamurai.Reset();
            rightSamurai.Reset();

            yield return new WaitForSeconds(1);
            SetIntroState();
        }

        IEnumerator EndMinigame(int results)
        {
            if (results == 1) {
                actionDisplay.text = "Left wins!";
                PlayersManager.result = PlayersManager.Result.LeftWin;
            } else
            if (results == 2) {
                actionDisplay.text = "Right wins!";
                PlayersManager.result = PlayersManager.Result.RightWin;
            } else
            {
                actionDisplay.text = "Draw!";
                PlayersManager.result = PlayersManager.Result.Draw;
            }

            yield return new WaitForSeconds(1);

            StartCoroutine(ModeManager.TransitionFromMinigame());
        }

        IEnumerator TimeOutTimer()
        {
            yield return new WaitForSeconds(4);
            actionDisplay.text = "LENTOS DEMAIS!";
            DuelResults(0);
        }

        #endregion

    }
}
