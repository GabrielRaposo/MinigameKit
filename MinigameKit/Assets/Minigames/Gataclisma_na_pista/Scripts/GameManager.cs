using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GataclismaNaPista
{
    public class GameManager : MonoBehaviour
    {

        public int BPM;
        ArrowSequence[] allArrowSequences;

        private void Awake()
        {
            allArrowSequences = GameObject.FindObjectsOfType<ArrowSequence>();
        }

        private void Start()
        {
            StartCoroutine(spawnAllArrows());
        }

        IEnumerator spawnAllArrows()
        {
            while (true)
            {
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