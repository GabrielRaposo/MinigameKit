using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeManager {
    public enum GameState
    {
        FreePlay,
        Medley
    }
    static public GameState State;

    static public IEnumerator TransitionToMenu()
    {
        string menuScreenCodename;
        switch (State)
        {
            default:
            case GameState.FreePlay:
                menuScreenCodename = MenuIndexList.MinigameHub();
                break;

            case GameState.Medley:
                menuScreenCodename = MenuIndexList.MedleyScreen();
                break;
        }
        MenuController.FirstScreen = menuScreenCodename;

        AsyncOperation sceneLoad;
        sceneLoad = SceneManager.LoadSceneAsync(0);
        sceneLoad.allowSceneActivation = false;
        while (sceneLoad.progress < .9f)
        {
            yield return null;
        }
        sceneLoad.allowSceneActivation = true;
    }
}
