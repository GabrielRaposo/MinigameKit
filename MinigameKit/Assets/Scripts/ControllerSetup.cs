using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSetup : MonoBehaviour {

    public Text leftPlayer, rightPlayer;
    public Button startButton;
    
	void Awake () {
		if (ControllerManager.instance != null) {
            ControllerManager.instance.ResetSetup();
        }
        leftPlayer.text = "";
        rightPlayer.text = "";
        startButton.interactable = false;
    }
	
	void Update () {
        if (ControllerManager.instance != null) {
            if (leftPlayer.text == "" || rightPlayer.text == "") {
                ControllerManager.instance.ControllerSetup();
                leftPlayer.text = ControllerManager.instance.GetLeftController();
                rightPlayer.text = ControllerManager.instance.GetRightController();
            } else {
                startButton.interactable = true;
            }
        }
    }
}
