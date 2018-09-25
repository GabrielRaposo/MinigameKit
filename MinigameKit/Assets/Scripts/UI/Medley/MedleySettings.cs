using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedleySettings : MonoBehaviour {
    const int
        MIN_ROUNDS = 1,
        MAX_ROUNDS = 3,
        MIN_PLAYERS = 2,
        MAX_PLAYERS = 4;

    public TextMeshProUGUI displayRounds;
    public TextMeshProUGUI displayPlayers;

    public static int rounds = 1;
    public static int nPlayers = 3;

    void Start () {
        UpdateRoundsDisplay();
        UpdatePlayersDisplay();
	}

    public void UpdateRoundsDisplay()
    {
        if (displayRounds) displayRounds.text = rounds.ToString();
    }

    public void UpdatePlayersDisplay()
    {
        if (displayPlayers) displayPlayers.text = nPlayers.ToString();
    }

    public void ChangeRoundsValue(int value)
    {
        rounds += value;
        if (rounds < MIN_ROUNDS) rounds = MAX_ROUNDS;
        if (rounds > MAX_ROUNDS) rounds = MIN_ROUNDS;

        UpdateRoundsDisplay();
    }

    public void ChangePlayersValue(int value)
    {
        nPlayers += value;
        if (nPlayers < MIN_PLAYERS) nPlayers = MAX_PLAYERS;
        if (nPlayers > MAX_PLAYERS) nPlayers = MIN_PLAYERS;

        UpdatePlayersDisplay();
    }
}
