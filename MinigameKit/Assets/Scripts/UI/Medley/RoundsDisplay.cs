using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RoundsDisplay : MonoBehaviour {

    [Header("References")]
    public TextMeshProUGUI roundDisplay;
    public TextMeshProUGUI turnDisplay;

    public static int round { get; private set; }
    public static int maxRound { get; private set; }
    public static int turn { get; private set; }
    public static int maxTurn  { get; private set; }

    MedleyManager medleyManager;

    public void Call(MedleyManager medleyManager)
    {
        this.medleyManager = medleyManager;

        maxRound = MedleySettings.rounds;
        maxTurn = QuantityOfTurns(MedleySettings.nPlayers);

        StartCoroutine(ShowDisplays());
    }

    private int QuantityOfTurns(int n)
    {
        int i = 0;
        while(n > 0) {
            n--;
            i += n;
        }
        return i;
    }

    public void NextTurn()
    {
        turn++;
    }

    public bool CheckTurn()
    {
        if (maxTurn < 1) return false;

        if (turn >= maxTurn)
        {
            return true;
        }
        return false;
    }

    private IEnumerator ShowDisplays()
    {
        roundDisplay.text = "Round " + (round + 1) + "/" + maxRound;
        turnDisplay.text = "Turn " + (turn + 1) +"/" + maxTurn;

        RectTransform roundRect = roundDisplay.rectTransform;
        RectTransform turnRect = turnDisplay.rectTransform;

        roundRect.anchoredPosition = new Vector3(600, roundRect.anchoredPosition.y);
        turnRect.anchoredPosition  = new Vector3(-600, turnRect.anchoredPosition.y);

        roundRect.DOLocalMoveX(0, .5f);
        turnRect.DOLocalMoveX(0, .5f);

        yield return new WaitForSeconds(2);

        medleyManager.CallNextDisplay();
    }
}
