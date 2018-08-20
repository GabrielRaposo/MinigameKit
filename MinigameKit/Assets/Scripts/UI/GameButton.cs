using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameButton : ButtonManager
{
	[Header("Game Parameters")]
    [SerializeField] TutorialObject tutorialInfo;

	[Header("Tutorial Screen Reference")]
	[SerializeField] TutorialScreen tutorial;
	[SerializeField] MenuController menuController;

	public override void Press()
	{
		menuController.EnableOverlay("tutorial");
        if (tutorialInfo)
        {
            tutorial.Tutorial(
                tutorialInfo.codename, tutorialInfo.minigameName, tutorialInfo.gameRules, tutorialInfo.image
            );
        }
        aSource.PlayOneShot(pressSound);
	}
}
