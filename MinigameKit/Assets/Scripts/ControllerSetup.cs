using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControllerSetup : MonoBehaviour {

    public TextMeshProUGUI leftPlayer, rightPlayer;
    public Button returnButton;

    void OnEnable () {
        ControllerManager.instance.ResetSetup();
        leftPlayer.text = "";
        rightPlayer.text = "";
        if(returnButton) returnButton.interactable = false;
    }
	
	void Update () {
        if (ControllerManager.instance != null) {
            if (leftPlayer.text == "" || rightPlayer.text == "") {
                ControllerManager.instance.ControllerSetup();
                leftPlayer.text = ControllerManager.instance.GetLeftController();
                rightPlayer.text = ControllerManager.instance.GetRightController();
            } else {
                if (returnButton) returnButton.interactable = true;
            }
        }
    }
}
