using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MedleyEndgame : MonoBehaviour {

    public TextMeshProUGUI winnersDisplay;
    public MedleyController medleyController;
    MedleyManager medleyManager;

    public void Call(MedleyManager medleyManager, List<string> winners)
    {
        this.medleyManager = medleyManager;
 
        winnersDisplay.text = string.Empty;
        foreach (string s in winners)
        {
            if(winnersDisplay.text != string.Empty)
            {
                winnersDisplay.text += " & ";
            }
            winnersDisplay.text += s;
        }
        StartCoroutine(WinResultsAnimation());
    }

    IEnumerator WinResultsAnimation()
    {
        yield return new WaitForSeconds(3);
        medleyController.SwitchMenu("title");
    }
}
