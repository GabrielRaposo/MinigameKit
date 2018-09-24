using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GataclismaNaPista
{
    public class CatPlayer : PlayerInfo
    {
        //Gambiarra esse scoreBar. É pra trocar a cor do scoreBar de acordo com o player.
        public Image scoreBar;

        public override void Start()
        {
            base.Start();

            GetComponentInChildren<CatAnimation>().color = base.color;
            scoreBar.color = color;
        }
    }

}
