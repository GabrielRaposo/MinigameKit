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

        public GameObject crown;
        public int Winner { get; private set; }
        /* Winner = 1 -> direita
         * Winner = -1 -> esquerda
         * Winner = 0 -> empate */

        private Vector2 crownPosition;
        private Slider slider;
        private float scoreLeft;
        private float scoreRight;

        PlayerController[] players;
        private void Awake()
        {
            slider = this.GetComponent<Slider>();
            players = GameObject.FindObjectsOfType<PlayerController>();
        }

        private void Start()
        {
            crownPosition = crown.transform.position;
        }

        public void UpdateScorebar()
        {
            scoreLeft = players[0].Score;
            scoreRight = players[1].Score;
            slider.DOValue(slider.maxValue * scoreLeft / (scoreLeft + scoreRight), 0.2f);

            // CROWN
            if(players[0].Score == players[1].Score)
            {
                crown.SetActive(false);
                this.Winner = 0;
            }
            else
            {
                crown.SetActive(true);
                if (players[0].Score > players[1].Score)
                {
                    crown.transform.position = crownPosition;
                    this.Winner = -1;
                }
                else if (players[1].Score > players[0].Score)
                {
                    crown.transform.position = crownPosition * new Vector2(-1, 1);
                    this.Winner = 1;
                }
            }
        }
    }
}