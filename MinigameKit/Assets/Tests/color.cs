using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class color : MonoBehaviour {

    public int player;
	
	void Start () {
        GetComponent<Image>().color = PlayersManager.playerColor[player];
    }
}
