using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bailarinas {

    public class BailarinaGameControler : MonoBehaviour {

        public LayoutTextManager layoutText;
        public UnityEngine.UI.Image clockImage;

        public GameObject leftP;
        public GameObject rightP;

        public float clock = 0;
        float totalTime = 50.0f;

        private void Start()
        {
            StartCoroutine(PreGame());

        }

        IEnumerator PreGame()
        {
            StartCoroutine(layoutText.DownText("3", 1.0f));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(layoutText.DownText("2", 1.0f));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(layoutText.DownText("1", 1.0f));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(layoutText.DownText("VAI!", 0.4f));
            yield return new WaitForSeconds(0.4f);
            layoutText.ClearText();

            StartGame();

        }

        void StartGame()
        {
            leftP.GetComponent<BailarinaScript>().enabled = true;
            rightP.GetComponent<BailarinaScript>().enabled = true;

            leftP.GetComponent<Rigidbody>().useGravity = true;
            rightP.GetComponent<Rigidbody>().useGravity = true;

            StartCoroutine(Clock());

        }

        IEnumerator Clock()
        {
            do
            {
                clock += Time.deltaTime;
                clockImage.fillAmount = (totalTime - clock) / totalTime;
                yield return null;

            } while (clock < totalTime);
            
        }

        //IEnumerator StartGame()
        //{

        //}

        //IEnumerator EndGame(PlayersManager.Result endResult)
        //{

        //}

    }

}



