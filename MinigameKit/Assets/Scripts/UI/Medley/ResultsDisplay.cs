using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultsDisplay : MonoBehaviour {

    public TextMeshProUGUI textDisplay;

    MedleyManager medleyManager;

	public void Call(MedleyManager medleyManager)
    {
        this.medleyManager = medleyManager;
        SetText(PlayersManager.result);
        StartCoroutine(ResultAnimation());
    }

    private void SetText(PlayersManager.Result result)
    {
        switch (result)
        {
            case PlayersManager.Result.Draw:
                textDisplay.text = "It's a draw!";
                break;

            case PlayersManager.Result.LeftWin:
                textDisplay.text = "Left player won!";
                medleyManager.AddScoreToPlayer(MedleyRandomizer.currentMatchup.leftPlayer);
                break;

            case PlayersManager.Result.RightWin:
                textDisplay.text = "Right player won!";
                medleyManager.AddScoreToPlayer(MedleyRandomizer.currentMatchup.rightPlayer);
                break;
        }
    }

    IEnumerator ResultAnimation()
    {
        yield return new WaitForSeconds(2);

        medleyManager.CallNextDisplay();
    }
}
