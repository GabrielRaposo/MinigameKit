using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour {

    // Cores escolhidas por Julio num color picker (nao sao finais)
    public static Color[] playerColor = new Color[] {
            new Color(.957f, .259f, .259f),    // Vermelho
            new Color(.259f, .678f, .957f),    // Azul Claro
            new Color(.957f, .922f, .259f),    // Amarelo
            new Color(.137f, .627f, .184f)     // Verde Escuro
        };

    public static int currentLeftPlayer;
    public static int currentRightPlayer;
    /// <summary>
    /// 0 = no results yet
    /// 1 = Left Player Victory
    /// 2 = Right Player Victory
    /// 3 = DRAW
    /// </summary>
    public static int result;
}
