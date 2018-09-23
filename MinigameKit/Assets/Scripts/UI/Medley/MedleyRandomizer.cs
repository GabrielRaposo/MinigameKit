using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class MedleyRandomizer : MonoBehaviour {

    public MedleyController medleyController;
    public TutorialObject[] minigameOptions;
    public GameObject confirmationPanel;

    [Header("Randomizer references")]
    public PlayerIcon leftPlayerIcon;
    public PlayerIcon rightPlayerIcon;
    public GameButton minigameIcon;
    public TextMeshProUGUI minigameTitle;

    public static readonly int maxNumberOfPlayers = 4;
    MedleyManager medleyManager;

    public struct Matchup
    {
        public int leftPlayer;
        public int rightPlayer;

        public Matchup(int left, int right)
        {
            leftPlayer = left;
            rightPlayer = right;
        }

        public override string ToString()
        {
            return "P" + leftPlayer + " vs P" + rightPlayer;
        }
    }
    private static Stack<Matchup> matchupStack;
    public static Matchup currentMatchup;

    private int nPLayers;
    private PlayerIcon currentLeftPlayer;
    private PlayerIcon currentRightPlayer;
    private TutorialObject currentMinigame;

    public void Call (MedleyManager medleyManager, bool reroll)
    {
        this.medleyManager = medleyManager;

        nPLayers = MedleySettings.nPlayers;
        if(reroll) GenerateMatchups();

        StartCoroutine(ShuffleAnimation());
    }

    private void GenerateMatchups()
    {
        Debug.Log("Turn 1 - Matchup Stack created.");

        matchupStack = new Stack<Matchup>();
        for (int i = 0; i < nPLayers; i++)
        {
            for (int j = i + 1; j < nPLayers; j++)
            {
                matchupStack.Push(new Matchup(i, j));
            }
        }

        // Shuffle
        matchupStack = new Stack<Matchup>(matchupStack.OrderBy(x => Random.Range(-100, 100)));
    }

    // Gera os matchups entre os jogadores que porventura empataram ao termino dos matchups originais.
    private void GenerateMatchups(int[] tiedPlayers)
    {
        matchupStack = new Stack<Matchup>();
        for (int i = 0; i < tiedPlayers.Length; i++)
        {
            for (int j = i + 1; j < tiedPlayers.Length; j++)
            {
                matchupStack.Push(new Matchup(tiedPlayers[i], tiedPlayers[j]));
            }
        }

        // Shuffle
        matchupStack = new Stack<Matchup>(matchupStack.OrderBy(x => Random.Range(-100, 100)));
    }

    private TutorialObject GetRandomMinigame()
    {
        int index = Random.Range(0, minigameOptions.Length);
        return minigameOptions[index];
    }

    private IEnumerator ShuffleAnimation()
    {
        SetUIMatchup(currentMatchup = matchupStack.Pop());

        yield return new WaitForSeconds(1f);

        confirmationPanel.GetComponent<PlayerConfirmationPanel>().Call(this);
        //medleyManager.CallNextDisplay();
    }

    private void SetUIMatchup(Matchup matchup)
    {
        currentLeftPlayer = medleyManager.GetPlayerAt(matchup.leftPlayer);
        leftPlayerIcon.Init(currentLeftPlayer.title, currentLeftPlayer.score, currentLeftPlayer.color);

        currentRightPlayer = medleyManager.GetPlayerAt(matchup.rightPlayer);
        rightPlayerIcon.Init(currentRightPlayer.title, currentRightPlayer.score, currentRightPlayer.color);

        currentMinigame = GetRandomMinigame();
        minigameTitle.text = currentMinigame.minigameName;
    }

    public void CallMinigame()
    {
        medleyController.EnableOverlay("tutorial");
        minigameIcon.tutorialInfo = currentMinigame;
        minigameIcon.Press();
    }
}
