using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MedleyManager : MonoBehaviour {

    struct matchup {
        public int leftPlayer;
        public int rightPlayer;

        public matchup (int left, int right) {
            leftPlayer = left;
            rightPlayer = right;
        }

        public override string ToString() {
            return "P" + leftPlayer + " vs P" + rightPlayer;
        }
    }

    public static readonly int maxNumberOfPlayers = 4;

    public static int maxScore;
    public static int numberOfPlayers;
    public static int[] playerScore;
    private static Stack<matchup> matchupStack;
    
    /// <summary>
    /// Gera uma lista limpa de pontuações para os jogadores atuais. Deve ser chamada ao iniciar o modo.
    /// </summary>
    void ResetScore() {
        playerScore = new int[numberOfPlayers];
        for (int i=0; i<numberOfPlayers; i++)
            playerScore[i] = 0;
    }

    /// <summary>
    /// Gera os matchups totais entre todos os jogadores, sem duplicatas.
    /// </summary>
    public static void GenerateMatchups() {
        matchupStack = new Stack<matchup>();
        for (int i = 0; i < numberOfPlayers; i++) {
            for (int j = i+1; j < numberOfPlayers; j++) {
                matchupStack.Push(new matchup(i, j));
            }
        }

        // Shuffle
        matchupStack = new Stack<matchup>(matchupStack.OrderBy(x => Random.Range(-100, 100)));
    }

    /// <summary>
    /// Gera os matchups entre os jogadores que porventura empataram ao termino dos matchups originais.
    /// </summary>
    /// <param name="tiedPlayers">Array de inteiros identificadores de cada jogador (comecando em 0)</param>
    public static void GenerateMatchups(int[] tiedPlayers) {
        matchupStack = new Stack<matchup>();
        for (int i = 0; i < tiedPlayers.Length; i++) {
            for (int j = i + 1; j < tiedPlayers.Length; j++) {
                matchupStack.Push(new matchup(tiedPlayers[i], tiedPlayers[j]));
            }
        }

        // Shuffle
        matchupStack = new Stack<matchup>(matchupStack.OrderBy(x => Random.Range(-100, 100)));
    }

    /// <summary>
    /// Pega o proximo matchup da pilha gerada e coloca os respectivos jogadores nas variaveis estaticas correspondentes.
    /// </summary>
    public static void PickNextMatchup() {
        matchup m = matchupStack.Pop();
        PlayersManager.currentLeftPlayer = m.leftPlayer;
        PlayersManager.currentRightPlayer = m.rightPlayer;
    }

    /// <summary>
    /// Funcao de teste para conferir a aleatoriedade do embaralhamento de matchups.
    /// </summary>
    private void ShuffleTest() {
        List<int> l = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        foreach (int i in l.ToArray())
            print(i);
        l = new List<int>(l.OrderBy(x => Random.Range(-100, 100)));
        print("-----");
        foreach (int i in l.ToArray())
            print(i);
    }

    /// <summary>
    /// Chamar depois de concluido um minigame. Encapsula a atualizacao de pontos dos envolvidos.
    /// </summary>
    private void UpdateScore() {
        if (PlayersManager.result == 1) {
            playerScore[PlayersManager.currentLeftPlayer]++;
        }
        else if (PlayersManager.result == 2) {
            playerScore[PlayersManager.currentRightPlayer]++;
        }
        else {
            // Caso empate de ponto para ambos:
            // playerScore[PlayersManager.currentLeftPlayer]++;
            // playerScore[PlayersManager.currentRightPlayer]++;
        }
    }
}
