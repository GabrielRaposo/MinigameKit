using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe que deve ser extendida pela classe a ser colocada no jogador de um minigame.
/// Se for necessario dar override no Start(), deve chamar base.Start() no comeco do novo Start()
/// </summary>
public class PlayerInfo : MonoBehaviour {

    /// <summary>
    /// ID do player. Eh um dos settados em PlayersManager antes do minigame comecar.
    /// </summary>
    protected int playerId;
    /// <summary>
    /// False = esquerda; True = direita
    /// </summary>
    [SerializeField] private bool side;
    /// <summary>
    /// Strings com os axis e botao de acao para controlar esse jogador.
    /// </summary>
    public PlayerButtons playerButtons;
    protected Color color;


    public virtual void Start() {
        if (ControllerManager.instance != null) {
            if (side) {
                playerButtons = ControllerManager.instance.GetRightButtons();
                playerId = PlayersManager.currentRightPlayer;
            } else {
                playerButtons = ControllerManager.instance.GetLeftButtons();
                playerId = PlayersManager.currentLeftPlayer;
            }
            Debug.Log("Debug");
        }
        else {
            if (side) {
                playerButtons = new PlayerButtons("K2");
                playerId = 2;
            } else {
                playerButtons = new PlayerButtons("K1");
                playerId = 1;
            }
        }
        color = PlayersManager.playerColor[playerId];
    }
}
