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
        public SpriteRenderer catSprite;

        public override void Start()
        {
            base.Start();
            
            scoreBar.color = base.color;
            catSprite.color = base.color;
        }
    }

}
