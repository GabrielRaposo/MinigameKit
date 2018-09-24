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

        private void Awake()
        {
            allArrowSequences = GameObject.FindObjectsOfType<ArrowSequence>();
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
            float musicLength = GetComponent<AudioSource>().clip.length; //deveria ser o numero de beats da musica
            int beats = 0;
            while (true)
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
        }
    }
}