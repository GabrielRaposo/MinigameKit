using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GataclismaNaPista
{
    public class ScoreCalculation : MonoBehaviour {


        /*TODO: update do score com evento OnScoreChange()*/

        private Slider slider;
        private float scoreLeft;
        private float scoreRight;

        PlayerController[] players;
        private void Awake()
        {
            slider = this.GetComponent<Slider>();
            players = GameObject.FindObjectsOfType<PlayerController>();
        }

        void Update()
        {
            scoreLeft = players[0].Score;
            scoreRight = players[1].Score;
            slider.value = slider.maxValue * scoreLeft / (scoreLeft + scoreRight);
        }
    }
}