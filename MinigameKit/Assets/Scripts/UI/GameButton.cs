using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : ButtonManager
{
	[Header("Game Parameters")]
    public TutorialObject tutorialInfo;

	[Header("Tutorial Screen Reference")]
	[SerializeField] TutorialScreen tutorial;

	public override void Press()
	{
        if (tutorialInfo)
        {
            tutorial.GetInfo (
                tutorialInfo.codename, 
                tutorialInfo.minigameName, 
                tutorialInfo.gameRules, 
                tutorialInfo.controls,
                tutorialInfo.image
            );
        } 
        aSource.PlayOneShot(pressSound);
	}
}
