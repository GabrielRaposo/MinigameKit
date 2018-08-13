using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeManager {
    public enum GameState
    {
        SinglePlay,
        Medley
    }
    static public GameState State;

    static public IEnumerator TransitionToMenu()
    {
        AsyncOperation sceneLoad;
        switch (State)
        {
            default:
            case GameState.SinglePlay:
                sceneLoad = SceneManager.LoadSceneAsync(ScenesIndexList.MinigameHub());
                break;

            case GameState.Medley:
                sceneLoad = SceneManager.LoadSceneAsync(ScenesIndexList.MedleyScreen());
                break;
        }

        sceneLoad.allowSceneActivation = false;
        while (sceneLoad.progress < .9f)
        {
            yield return null;
        }
        sceneLoad.allowSceneActivation = true;
    }
}
