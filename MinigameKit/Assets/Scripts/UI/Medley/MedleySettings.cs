using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedleySettings : MonoBehaviour {

    public TextMeshProUGUI displayRounds;
    public TextMeshProUGUI displayPlayers;

    public static int rounds = 1;
    public static int nPlayers = 4;

    void Start () {
        UpdateRoundsDisplay();
        UpdatePlayersDisplay();
	}

    public void UpdateRoundsDisplay()
    {
        if (displayRounds) displayRounds.text = "Rounds: " + rounds.ToString();
    }

    public void UpdatePlayersDisplay()
    {
        if (displayPlayers) displayPlayers.text = "Players: " + nPlayers.ToString();
    }
}
