using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Comidinhas
{
    public class GameController : MonoBehaviour {

        public static bool startGame = false;
        public static bool startCount = false;
        public float readyTime = 3f;
        public float time = 30f;

        public TextMeshProUGUI timerText;
        public TextMeshProUGUI readyTimerText;

        public Player leftFatty;
        public Player rightFatty;
        public FoodGen foodGen;

        void Start () {
            timerText.text = null;

            StartCoroutine(ReadyTimer());
	    }
	
	    
	    void FixedUpdate () {

            if (startGame)
            {
                time -= Time.deltaTime;
                timerText.text = time.ToString("0");
                
                if(time <= 0)
                {
                    startGame = false;
                    WinGame();
                }
            }
            else
            {
                if (startCount)
                {
                    readyTime -= Time.deltaTime;
                    if (readyTime >= 0.5f) readyTimerText.text = readyTime.ToString("0");
                    else readyTimerText.text = "GO";
                }
            }
	    }

        void WinGame()
        {
            leftFatty.ToggleMovement(false);
            rightFatty.ToggleMovement(false);
            foodGen.SetActivation(false);

            if (leftFatty.score > rightFatty.score) {
                Debug.Log("Esquerda é o vencedor!");
                timerText.text = "Left wins!";
                PlayersManager.result = PlayersManager.Result.LeftWin;
            } else
            if (leftFatty.score < rightFatty.score) {
                Debug.Log("Direita é o vencedor!");
                timerText.text = "Right wins!";
                PlayersManager.result = PlayersManager.Result.RightWin;
            } else
            {
                Debug.Log("Empate!");
                timerText.text = "Draw...";
                PlayersManager.result = PlayersManager.Result.Draw;
            }

            StartCoroutine(EndGameAnimation());
        }

        IEnumerator ReadyTimer()
        {
            startCount = true;

            yield return new WaitForSeconds(3);

            startCount = false;
            startGame = true;
            readyTimerText.enabled = false;

            leftFatty.ToggleMovement(true);
            rightFatty.ToggleMovement(true);
            foodGen.SetActivation(true);

            StopCoroutine(ReadyTimer());
        }

        IEnumerator EndGameAnimation()
        {
            yield return new WaitForSeconds(1);

            StartCoroutine(ModeManager.TransitionFromMinigame());
        } 
    }
}

