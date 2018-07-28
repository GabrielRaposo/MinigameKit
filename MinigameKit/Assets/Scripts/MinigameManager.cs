using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameManager : MonoBehaviour {

    /// <summary>
    /// Lista publica com os nomes de todos os minigames no jogo. Esses nomes sao usados para identificar seus diretorios.
    /// </summary>
    public static string[] minigameNameList;
    /// <summary>
    /// Lista publica com os nomes finais de todos os minigames no jogo. Esses nomes sao usados nos Buttons para seleciona-los.
    /// </summary>
    public static string[] minigameDisplayNameList;
    /// <summary>
    /// Nome do proximo minigame que sera aberto.
    /// </summary>
    public static string nextMinigame;
    /// <summary>
    /// Lista de minigames ja usados. Usada para evitar minigames repetidos.
    /// </summary>
    private List<string> usedMinigames = new List<string>();

	void Awake () {
        if (minigameNameList == null || minigameNameList.Length < 1) {
            var minigameList = Resources.LoadAll<TutorialObject>("Tutorials");
            minigameNameList = new string[minigameList.Length];
            minigameDisplayNameList = new string[minigameList.Length];
            for (int i = 0; i < minigameList.Length; i++) {
                minigameNameList[i] = minigameList[i].name.Substring(0, minigameList[i].name.Length - 8);
                minigameDisplayNameList[i] = minigameList[i].minigameName;
            }
        }

    }

    /// <summary>
    /// Retorna um minigame aleatorio. Esse minigame eh adicionado ao final da lista de minigames ja usados.
    /// </summary>
    /// <param name="allowRepetition">Verdadeiro por padrao.
    /// Se falso, ignora o maximo de minigames possivel.
    /// Caso nao sobrem minigames, seleciona o mais antigo a ter sido jogado.</param>
    /// <returns></returns>
    public string RandomMinigame(bool allowRepetition = true) {
        if (allowRepetition) {
            int m = Random.Range(0, minigameNameList.Length);
            if (usedMinigames.Contains(minigameNameList[m]))
                usedMinigames.Remove(minigameNameList[m]);
            usedMinigames.Add(minigameNameList[m]);
            return minigameNameList[m];
        }
        else {
            if (minigameNameList.Length <= usedMinigames.Count) {
                string oldestMinigame = usedMinigames[0];
                usedMinigames.Remove(oldestMinigame);
                usedMinigames.Add(oldestMinigame);
                return oldestMinigame;
            } else {
                List<string> remainingMinigames = new List<string>(minigameNameList);
                foreach (string s in usedMinigames)
                    remainingMinigames.Remove(s);
                int m = Random.Range(0, remainingMinigames.Count);
                usedMinigames.Add(remainingMinigames[m]);
                return remainingMinigames[m];
            }
        }
    }

    /// <summary>
    /// Retorna um minigame aleatorio. Esse minigame eh adicionado ao final da lista de minigames ja usados.
    /// </summary>
    /// <param name="range">Ignora os range-esimos ultimos minigames jogados, se possivel.
    /// Caso nao sobrem minigames, seleciona o mais antigo a ter sido jogado.</param>
    /// <returns></returns>
    public string RandomMinigame(int range) {
        if (range >= usedMinigames.Count) return RandomMinigame(false);
        else if (range <= 0) return RandomMinigame(true);
        else {
            List<string> remainingMinigames = new List<string>(minigameNameList);
            for (int i = 0; i < range; i++)
                remainingMinigames.Remove(usedMinigames[usedMinigames.Count-1 - i]);
            print("Remaining: " + ArrayPrint(remainingMinigames.ToArray()));
            int m = Random.Range(0, remainingMinigames.Count);
            usedMinigames.Add(remainingMinigames[m]);
            return remainingMinigames[m];
        }
    }

    /// <summary>
    /// Abre o tutorial de um minigame
    /// </summary>
    /// <param name="tutorialName">Nome do minigame (nao eh case sensitive)</param>
    public void OpenMinigameTutorial(string minigameName) {
        nextMinigame = minigameName;
        SceneManager.LoadScene("MinigameTutorial");
    }

    /// <summary>
    /// Abre o minigame em si
    /// </summary>
    /// <param name="tutorialName">Nome do minigame (nao eh case sensitive)</param>
    public void OpenMinigame(string minigameName)
    {
        SceneManager.LoadScene(minigameName);
    }
    
    string ArrayPrint(string[] array) {
        var ret = "{ ";
        for (int i = 0; i < array.Length - 1; i++)
            ret += array[i] + ", ";
        ret += array[array.Length - 1] + " }";
        return ret;
    }

    public void TestStuff(int range) {
        minigameNameList = new string[] { "game1", "game2", "game3", "game4", "game5", "game6", "game7", "game8" };
        print("Primeiro: " + RandomMinigame(range));
        print("Usados: " + ArrayPrint(usedMinigames.ToArray()));
    }
}
