using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonModeSelect : MonoBehaviour {

    public ModeManager.GameState state;

	void Start ()
    {
        Button button = GetComponent<Button>();
        if (button) {
            button.onClick.AddListener( 
                () => ModeManager.State = state
            );
        } else Debug.Log("'Button' não pôde ser encontrado.");
	}

}
