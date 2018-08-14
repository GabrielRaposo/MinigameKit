using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Comidinhas
{
    public class GameController : MonoBehaviour {

        public bool startTimer = false;
        public float readyTime = 3f;
        public float time = 30f;
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI readyTimerText;

	    void Start () {

            timerText.text = null;

            StartCoroutine(ReadyTimer());

	    }
	
	    
	    void Update () {

            if (startTimer)
            {
                time -= Time.deltaTime;
                timerText.text = time.ToString("0");
            }
            else
            {
                readyTime -= Time.deltaTime;
                if (readyTime >= 0.5f) readyTimerText.text = readyTime.ToString("0");
                else readyTimerText.text = "GO";
            }

	    }

        IEnumerator ReadyTimer()
        {
           
            yield return new WaitForSeconds(3);
            startTimer = true;
            readyTimerText.enabled = false;
            StopCoroutine(ReadyTimer());
        }
    }
}

