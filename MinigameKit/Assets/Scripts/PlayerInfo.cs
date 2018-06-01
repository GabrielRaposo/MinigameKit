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
    protected PlayerButtons playerButtons;
    protected Color color;


    public virtual void Start() {
        if (side) {
            playerButtons = ControllerManager.instance.GetRightButtons();
            playerId = PlayersManager.currentRightPlayer;
        } else {
            playerButtons = ControllerManager.instance.GetLeftButtons();
            playerId = PlayersManager.currentLeftPlayer;
        }
        color = PlayersManager.playerColor[playerId];
    }
}
