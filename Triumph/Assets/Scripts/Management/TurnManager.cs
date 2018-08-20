using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnManager : MonoBehaviour {

    [Header("UI References")]
    public TextMeshProUGUI phaseDisplay;

    [Header("Organization")]
    public Arena arena;

    int turnCount;
    Team currentPhase;
    Team initialPhase;

	public void Init()
    {
        initialPhase = Team.A;
        currentPhase = initialPhase;
        UpdatePhase();
    }

    void UpdatePhase()
    {
        phaseDisplay.text = "Time " + (currentPhase == Team.A ? "A" : "B") + "!";
        phaseDisplay.gameObject.SetActive(true);
        phaseDisplay.GetComponent<Animator>().enabled = true;
        arena.EnableInput(currentPhase);
    }

    public void SwitchPhase()
    {
        if (currentPhase == Team.A) currentPhase = Team.B;
        else                        currentPhase = Team.A;
        if (currentPhase == initialPhase) turnCount++;

        UpdatePhase();
    }
}
