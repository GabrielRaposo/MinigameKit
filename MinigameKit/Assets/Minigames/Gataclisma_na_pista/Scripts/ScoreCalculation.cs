using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

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

        public void UpdateScorebar()
        {
            scoreLeft = players[0].Score;
            scoreRight = players[1].Score;
            slider.DOValue(slider.maxValue * scoreLeft / (scoreLeft + scoreRight), 0.2f);
        }
    }
}