using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MinigameTutorial : MonoBehaviour {

    public Text minigameName;
    public Text minigameDesc;
    public RawImage minigameThumbnail;
    public GameObject toRules, toControls;
    private TutorialObject tutorialObject;
    private bool state = true;
    
	void Start () {
        tutorialObject = Resources.Load<TutorialObject>("Tutorials/" + MinigameManager.nextMinigame + "Tutorial");
        minigameName.text = tutorialObject.codename;
        minigameDesc.text = tutorialObject.gameRules;
        minigameThumbnail.texture = tutorialObject.image;
	}
	
	void Update () {
        if (state && Input.GetAxisRaw("Horizontal") > 0) {
            ToControls();
        } else if (!state && Input.GetAxisRaw("Horizontal") < 0) {
            ToRules();
        } else if (Input.GetButtonDown("Submit")) {
            ToMinigame();
        }
	}
    
    public void ToControls() {
        //minigameDesc.text = tutorialObject.controls;
        toControls.SetActive(false);
        toRules.SetActive(true);
        state = !state;
    }
    public void ToRules() {
        minigameDesc.text = tutorialObject.gameRules;
        toControls.SetActive(true);
        toRules.SetActive(false);
        state = !state;
    }
    public void ToMinigame() {
        SceneManager.LoadScene(MinigameManager.nextMinigame);
    }
}
