using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour {

    // Cores escolhidas por Julio num color picker (nao sao finais)
    public static Color[] playerDefaultColor = new Color[] {
            new Color(.957f, .259f, .259f),    // Vermelho
            new Color(.259f, .678f, .957f),    // Azul Claro
            new Color(.957f, .922f, .259f),    // Amarelo
            new Color(.137f, .627f, .184f)     // Verde Escuro
    };
    public static Color[] playerColor = playerDefaultColor;

    public static int currentLeftPlayer = 0;
    public static int currentRightPlayer = 1;

    public enum Result { Draw, LeftWin, RightWin }
    public static Result result;
}
