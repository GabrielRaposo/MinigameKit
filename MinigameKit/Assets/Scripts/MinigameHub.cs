using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameHub : MonoBehaviour {

    public Transform interactableArea;
    [Tooltip("O prefab que indica como será cada botão")]
    public GameObject buttonPrefab;
    private GameObject[] buttonList;
    private MinigameManager mgm;
    
    void Awake() {
        mgm = GetComponent<MinigameManager>();
    }

	void Start () {
        buttonList = new GameObject[MinigameManager.minigameNameList.Length];
        for(int i = 0; i < MinigameManager.minigameNameList.Length; i++) {
            var i2 = i; // Gambiarra obrigatoria porque delegates
            buttonList[i] = (GameObject)Instantiate(buttonPrefab, interactableArea);
            buttonList[i].GetComponentInChildren<Text>().text = MinigameManager.minigameDisplayNameList[i];
            buttonList[i].GetComponent<Button>().onClick.AddListener(delegate { mgm.OpenMinigameTutorial(MinigameManager.minigameNameList[i2]); });
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
