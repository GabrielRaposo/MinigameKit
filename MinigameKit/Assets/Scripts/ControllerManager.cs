using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Estrutura que salva as strings de cada input de um dado controle
/// </summary>
public struct PlayerButtons {
    public string horizontal;
    public string vertical;
    public string action;

    public PlayerButtons(string controller) {
        horizontal = controller + "Horizontal";
        vertical = controller + "Vertical";
        action = controller + "Action";
    }
}

public class ControllerManager : MonoBehaviour {

    [SerializeField] private string leftController;
    [SerializeField] private string rightController;
    private PlayerButtons leftButtons;
    private PlayerButtons rightButtons;

    static public ControllerManager instance;
    
    void Awake () {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
        }
	}

    /// <summary>
    /// Caso alguem aperte o botao de Action, retorna o nome daquele controle.
    /// Util para designar um dado controle 
    /// </summary>
    /// <returns></returns>
    public string GetAnyActionButton() {
        for (int i = 1; i < 3; i++)
            if (Input.GetButtonDown("K" + i + "Action"))
                return "K" + i;
        for (int i = 1; i < 5; i++)
            if (Input.GetButtonDown("J" + i + "Action"))
                return "J" + i;
        return null;
    }
    
    /// <summary>
    /// Atribui os inputs de cada controle (esquerdo e direito).
    /// </summary>
    public void SetButtons() {
        leftButtons = new PlayerButtons(leftController);
        rightButtons = new PlayerButtons(rightController);
    }

    /// <summary>
    /// Ao fazer o setup de controles, fazer:
    /// leftController = null; rightController = null;
    /// E colocar essa funcao em um Update.
    /// </summary>
    public void ControllerSetup() {
        var controller = GetAnyActionButton();
        if (controller != null) {
            if (leftController == null) {
                leftController = controller;
                leftButtons = new PlayerButtons(leftController);
            } else if (rightController == null) {
                rightController = controller;
                rightButtons = new PlayerButtons(rightController);
            }
        }
    }

    public PlayerButtons GetLeftButtons() {
        return leftButtons;
    }
    
    public PlayerButtons GetRightButtons() {
        return rightButtons;
    }

}
