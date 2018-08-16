using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Comidinhas
{
    public class GameController : MonoBehaviour {

        public static bool startGame = false;
        public float readyTime = 3f;
        public float time = 30f;
        public float playerSpeed = 10f;

        public TextMeshProUGUI timerText;
        public TextMeshProUGUI readyTimerText;

        public Player leftFatty;
        public Vector3 leftFatty_x;
        public Player rightFatty;
        public Vector3 rightFatty_x;

        public bool playerColliding;
        float collisionImpulse;

        void Start () {

            timerText.text = null;

            StartCoroutine(ReadyTimer());

	    }
	
	    
	    void FixedUpdate () {

            if (startGame)
            {
                time -= Time.deltaTime;
                timerText.text = time.ToString("0");

                InputVerification();
            }
            else
            {
                readyTime -= Time.deltaTime;
                if (readyTime >= 0.5f) readyTimerText.text = readyTime.ToString("0");
                else readyTimerText.text = "GO";
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
            MovePlayer();

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
                collisionImpulse = 120f;
                rightFatty.GetComponent<Rigidbody2D>().AddForce(new Vector2(collisionImpulse, 0f), ForceMode2D.Impulse);
                Debug.Log("Lança Jogador da Direita para a Direita!");
            }

            if (Input.GetButtonDown(rightFatty.playerButtons.action) && playerColliding)
            {
                collisionImpulse = 120f;
                leftFatty.GetComponent<Rigidbody2D>().AddForce(new Vector2(-collisionImpulse, 0f), ForceMode2D.Impulse);
                Debug.Log("Lança Jogador da Esquerda para a Esquerda!");
            }
        }

        void ImpulseStop(Player pl)
        {
            pl.GetComponent<Rigidbody2D>().velocity *= 0.95f * Time.deltaTime;
            //pl.GetComponent<Rigidbody2D>().Sleep();
        }

        void MovePlayer()
        {
            leftFatty_x = new Vector3(Input.GetAxisRaw(leftFatty.playerButtons.horizontal), 0, 0);
            rightFatty_x = new Vector3(Input.GetAxisRaw(rightFatty.playerButtons.horizontal), 0, 0);

            leftFatty.transform.Translate(leftFatty_x * playerSpeed * Time.deltaTime);
            rightFatty.transform.Translate(rightFatty_x * playerSpeed * Time.deltaTime);
        }


        IEnumerator ReadyTimer()
        {
           
            yield return new WaitForSeconds(3);
            startGame = true;
            readyTimerText.enabled = false;
            StopCoroutine(ReadyTimer());
        }
    }
}

