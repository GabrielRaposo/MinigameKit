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

        Rigidbody2D leftFattyRB;
        Rigidbody2D rightFattyRB;

        public bool playerColliding;
        float collisionImpulse;

        void Start () {

            rightFattyRB = rightFatty.GetComponent<Rigidbody2D>();
            leftFattyRB = leftFatty.GetComponent<Rigidbody2D>();

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

                InputVerification();
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

        public void PlayerCollide(int obs)
        {
            switch (obs)
            {
                case 1:
                    playerColliding = true;
                    break;
                case 2:
                    playerColliding = false;
                    break;
            }
        }

        void InputVerification()
        {

            if (Input.GetAxisRaw(leftFatty.playerButtons.horizontal) > 0)
            {
                ImpulseStop(leftFatty);
            }
            if (Input.GetAxisRaw(rightFatty.playerButtons.horizontal) < 0)
            {
                ImpulseStop(rightFatty);
            }

            if (Input.GetButtonDown(leftFatty.playerButtons.action) && playerColliding)
            {
                //collisionImpulse = 120f;
                rightFattyRB.AddForce(new Vector2(230f, 0f), ForceMode2D.Impulse);
                Debug.Log("Lança Jogador da Direita para a Direita!");
            }

            if (Input.GetButtonDown(rightFatty.playerButtons.action) && playerColliding)
            {
                //collisionImpulse = 120f;
                leftFattyRB.AddForce(new Vector2(-230f, 0f), ForceMode2D.Impulse);
                Debug.Log("Lança Jogador da Esquerda para a Esquerda!");
            }
        }

        void ImpulseStop(Player pl)
        {
            pl.GetComponent<Rigidbody2D>().velocity *= 0.85f;
            //pl.GetComponent<Rigidbody2D>().Sleep();
        }


        void WinGame()
        {
            if (leftFatty.score > rightFatty.score)
            {
                //Esquerda vence o jogo
                Debug.Log("Esquerda é o vencedor!");
            }
            else
            {
                if (leftFatty.score < rightFatty.score)
                {
                    //Direita vence o jogo
                    Debug.Log("Direita é o vencedor!");
                }
                else
                {
                    //Empate
                    Debug.Log("Empate!");
                }
            }
        }


        IEnumerator ReadyTimer()
        {
            startCount = true;

            yield return new WaitForSeconds(3);

            startCount = false;
            startGame = true;
            readyTimerText.enabled = false;
            StopCoroutine(ReadyTimer());
        }
    }
}

