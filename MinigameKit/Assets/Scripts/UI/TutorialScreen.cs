using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialScreen : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI text;
	[SerializeField] RawImage image;

    string codename;

	public void Tutorial(string codename, string gameTitle, string tutorialText, Texture gameImage)
	{
        this.codename = codename;

        title.text = gameTitle;
		text.text = tutorialText;
		image.texture = gameImage;
	}

    public void StartMinigame()
    {
        //temp
        SceneManager.LoadScene("Minigames/" + codename + "/" + codename) ;
    }
}
