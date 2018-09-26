using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace GataclismaNaPista
{
    public class GameManager : MonoBehaviour
    {
        public int BPM;
        public Text text;

        private ArrowSequence[] allArrowSequences;

        //gambiarra
        private ScoreCalculation scoreCalculation;

        private void Awake()
        {
            allArrowSequences = GameObject.FindObjectsOfType<ArrowSequence>();
            scoreCalculation = GameObject.FindObjectOfType<ScoreCalculation>();
        }

        private void Start()
        {
            StartCoroutine(CountDown());
        }

        IEnumerator CountDown()
        {
            for(int i = 3; i > 0; i--)
            {
                text.text = i.ToString();
                yield return new WaitForSeconds(1);
            }
            text.text = "GO!";
            text.GetComponent<RectTransform>().DOMoveY(0.5f, 0.5f);
            text.DOColor(new Color(1, 1, 1, 0), 0.5f);
            StartCoroutine(SpawnAllArrows());
        }

        IEnumerator SpawnAllArrows()
        {
            int beats = 0;
            /*esses beats deveriam ser o numero de setas total da música, aqui eu fui contando e vi onde que era melhor parar
             * mas pode mudar se achar necessário */
            while (beats < 86)
            {
                Debug.Log("Beats: " + beats);
                beats++;
                Direction direction = (Direction)Random.Range(0, 4);
                //int duration = Random.Range(1, 3); //spawna setas de duracao entre 1 e 2
                int duration = 1;
                foreach (ArrowSequence sequence in allArrowSequences)
                {
                     sequence.SpawnArrow(direction, duration);
                }
                yield return new WaitForSeconds(60f / BPM * duration);
            }
            yield return new WaitForSeconds(2f);
            StartCoroutine(EndGame());
        }

        IEnumerator EndGame()
        {
            text.GetComponent<RectTransform>().DOMoveY(0.5f, 0.5f);
            text.DOColor(new Color(1, 1, 1, 1), 0.5f);
            text.resizeTextForBestFit = true;
            if(scoreCalculation.Winner > 0)
            {
                text.text = "DIREITA VENCE!";
                PlayersManager.result = PlayersManager.Result.RightWin;
            }
            else if(scoreCalculation.Winner < 0)
            {
                text.text = "ESQUERDA VENCE!";
                PlayersManager.result = PlayersManager.Result.LeftWin;
            }
            else
            {
                text.text = "EMPATE!";
                PlayersManager.result = PlayersManager.Result.Draw;
            }
            /*aqui deveria esperar um tempo e depois finalizar o jogo*/
            yield return new WaitForSeconds(5);
            
            StartCoroutine(ModeManager.TransitionFromMinigame());
        }
    }
}