using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerConfirmationPanel : MonoBehaviour {

    [Header("References")]
    public TextMeshProUGUI leftPlayerDisplay;
    public TextMeshProUGUI rightPlayerDisplay;

    [Header("Values")]
    public float alphaDisabled;
    public float alphaEnabled;

    bool leftPlayer;
    bool rightPlayer;
    MedleyRandomizer medleyRandomizer;
    PlayerButtons leftButton;
    PlayerButtons rightButton;

    bool hasBeenSetup;

    public void Call(MedleyRandomizer medleyRandomizer)
    {
        this.medleyRandomizer = medleyRandomizer;
        ControllerManager controllerManager = ControllerManager.instance;
        leftButton = controllerManager.GetLeftButtons();
        rightButton = controllerManager.GetRightButtons();
        SetDisplay(leftPlayerDisplay, leftPlayer = false);
        SetDisplay(rightPlayerDisplay, rightPlayer = false);
        hasBeenSetup = true;
    }
	
    private void SetDisplay(TextMeshProUGUI display, bool value)
    {
        Color color = display.color;
        if (value) {
            color.a = alphaEnabled;
            display.color = color;
            display.text = "Ready!";
        } else {
            color.a = alphaDisabled;
            display.color = color;
            display.text = "Press Action to confirm";
        }

        if(leftPlayer && rightPlayer)
        {
            medleyRandomizer.CallMinigame();
        }
    }

	void Update () {
        if (!hasBeenSetup) return;

        if (Input.GetButtonDown(leftButton.action))
        {
            SetDisplay(leftPlayerDisplay, leftPlayer = true);
        }
        if (Input.GetButtonDown(rightButton.action))
        {
            SetDisplay(rightPlayerDisplay, rightPlayer = true);
        }
    }

    private void OnDisable()
    {
        hasBeenSetup = false;
    }
}
