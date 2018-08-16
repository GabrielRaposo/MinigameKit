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


        void Start () {

            timerText.text = null;

            StartCoroutine(ReadyTimer());

	    }
	
	    
	    void Update () {

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


        void InputVerification()
        {
            leftFatty_x = new Vector3(Input.GetAxisRaw(leftFatty.playerButtons.horizontal), 0, 0);
            rightFatty_x = new Vector3(Input.GetAxisRaw(rightFatty.playerButtons.horizontal), 0, 0);

            leftFatty.transform.Translate(leftFatty_x * playerSpeed * Time.deltaTime);
            rightFatty.transform.Translate(rightFatty_x * playerSpeed * Time.deltaTime);

            //if (Input.GetButton(leftFatty.playerButtons.horizontal)) leftFatty.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime);
            //if (Input.GetButton(rightFatty.playerButtons.horizontal)) rightFatty.transform.Translate(Vector3.left * playerSpeed * Time.deltaTime);
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

