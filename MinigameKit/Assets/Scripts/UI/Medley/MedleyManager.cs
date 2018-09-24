using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//responsável pela troca e displays e lógica geral do modo
public class MedleyManager : MonoBehaviour {

    [System.Serializable]
    public class Display
    {
        public Transform displayTransform;
        public string next;
    }

    [Header("Players")]
    public Transform playersLayout;
    public GameObject playerPrefab;
    public Color[] playerColors;

    [Header("Displays")]
    public Display roundDisplay;
    public Display randomizerDisplay;
    public Display resultsDisplay;
    public Display endgameDisplay;

    [Header("Scripts")]
    public RoundsDisplay roundsScript;

    private List<PlayerIcon> players;
    private static List<int> playerScores;

    private Display currentDisplay;
    private EventSystem eventSystem;

    private static bool playing = false;

    //achar uma forma melhor de fazer isso depois
    private bool rerollTrigger;

	void Start ()
    {
        eventSystem = EventSystem.current;
        if (!playing) {
            rerollTrigger = true;
            CreatePlayerScores(MedleySettings.nPlayers);
            SwitchDisplay("Round Display");
        } else {
            SetPlayers();
            roundsScript.NextTurn();
            SwitchDisplay("Results Display");
        }
    }

    private void CreatePlayerScores(int quantity)
    {
        playerScores = new List<int>();

        for (int i = 0; i < quantity; i++)
        {
            playerScores.Add(0);
        }

        PlayersManager.playerColor = playerColors;
        SetPlayers();
    }
	
	private void SetPlayers()
    {
        int quantity = playerScores.Count;
        players = new List<PlayerIcon>();

        for (int i = 0; i < quantity; i++)
        {
            GameObject _object = Instantiate(playerPrefab, playersLayout);
            PlayerIcon icon = _object.GetComponent<PlayerIcon>();
            if (icon)
            {
                Color color = (i < playerColors.Length) ? playerColors[i] : Color.black;
                icon.Init("Player " + i, playerScores[i], color);
            }
            players.Add(icon);
        }


        playerPrefab.SetActive(false);
        playing = true;
    }

    public PlayerIcon GetPlayerAt(int index)
    {
        index %= players.Count;
        return players[index];
    }

    public void AddScoreToPlayer(int index, int value = 1)
    {
        playerScores[index] += value;
        players[index].AddScore(value);
    }

    public void SwitchDisplay(string tag)
    {
        if (currentDisplay != null) {
            currentDisplay.displayTransform.gameObject.SetActive(false);
        }
        switch (tag)
        {
            case "Round Display":
                if(roundsScript.CheckTurn())
                {
                    SwitchDisplay("Endgame");
                    break;
                }

                currentDisplay = roundDisplay;
                currentDisplay.displayTransform.gameObject.SetActive(true);
                roundsScript.Call(this);
                break;

            case "Randomizer":
                currentDisplay = randomizerDisplay;
                currentDisplay.displayTransform.gameObject.SetActive(true);
                currentDisplay.displayTransform.GetComponent<MedleyRandomizer>().Call(this, rerollTrigger);
                rerollTrigger = false;
                break;

            case "Results Display":
                currentDisplay = resultsDisplay;
                currentDisplay.displayTransform.gameObject.SetActive(true);
                currentDisplay.displayTransform.GetComponent<ResultsDisplay>().Call(this);
                break;

            case "Endgame":
                currentDisplay = endgameDisplay;
                currentDisplay.displayTransform.gameObject.SetActive(true);
                currentDisplay.displayTransform.GetComponent<MedleyEndgame>().Call(this, GetWinners());
                break;

            default:
                Debug.Log("Display com tag " + tag + " não pôde ser encontrado.");
                return;
        }       
    }

    private List<string> GetWinners()
    {
        List<string> winners = new List<string>();
        int highestScore = -1;
        for(int i = 0; i < playerScores.Count; i++)
        {
            if(playerScores[i] > highestScore)
            {
                winners = new List<string>();
                winners.Add(players[i].title);
                highestScore = playerScores[i];
            } else 
            if(playerScores[i] == highestScore)
            {
                winners.Add(players[i].title);
                highestScore = playerScores[i];
            }
        }
        return winners;
    }

    public void CallNextDisplay()
    {
        SwitchDisplay(currentDisplay.next);
    }
}
