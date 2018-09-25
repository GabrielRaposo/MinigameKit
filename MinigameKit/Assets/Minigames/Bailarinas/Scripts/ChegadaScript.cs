using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bailarinas
{

    public class ChegadaScript : MonoBehaviour {

        private BailarinaScript closerPlayer;        

        public BailarinaScript playerLeft;
        public BailarinaScript playerRight;

        private void Start()
        {
            playerLeft.onFall += () => OnPlayerFall(playerLeft);
            playerRight.onFall += () => OnPlayerFall(playerRight);
        }

        private void Update()
        {
            //closerPlayer = CheckCloser();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<BailarinaScript>() == playerLeft)
            {
                EndMinigame(PlayersManager.Result.LeftWin);
            }

            if (other.GetComponent<BailarinaScript>() == playerRight)
            {
                EndMinigame(PlayersManager.Result.RightWin);
            }

        }        

        BailarinaScript CheckCloser()
        {
            if (playerLeft.transform.position.z > playerRight.transform.position.z)
            {
                return playerLeft;
            }
            else if (playerRight.transform.position.z > playerLeft.transform.position.z)
            {
                return playerRight;
            }
            else
            {
                return null;
            }
        }

        void EndMinigame(PlayersManager.Result result)
        {
            Debug.Log(result.ToString());

            if (result == PlayersManager.Result.LeftWin)
            {
                PlayersManager.result = PlayersManager.Result.LeftWin;
                playerLeft.Win();
                playerRight.Die();
            }
            else if (result == PlayersManager.Result.RightWin)
            {
                PlayersManager.result = PlayersManager.Result.RightWin;
                playerLeft.Die();
                playerRight.Win();
            }
            else
            {
                PlayersManager.result = PlayersManager.Result.Draw;
                Destroy(playerRight);
                Destroy(playerLeft);
            }

        }

        void OnPlayerFall(BailarinaScript bai)
        {
            if(bai == playerRight)
            {
                if (playerLeft.dead)
                {
                    EndMinigame(PlayersManager.Result.Draw);
                }
                else
                {
                    EndMinigame(PlayersManager.Result.LeftWin);
                }
            }
            else
            {
                if (playerRight.dead)
                {
                    EndMinigame(PlayersManager.Result.Draw);
                }
                else
                {
                    EndMinigame(PlayersManager.Result.RightWin);
                }
            }
        }

        public void OnTimeEnd()
        {
            
        }

    }
}
