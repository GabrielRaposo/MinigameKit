﻿using System.Collections;
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
        int sceneIndex;
        switch (State)
        {
            default:
            case GameState.FreePlay:
                MenuController.FirstScreen = "freeplay";
                sceneIndex = 0;
                break;

            case GameState.Medley:
                sceneIndex = 1;
                break;
        }

        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync(sceneIndex);
        sceneLoad.allowSceneActivation = false;
        while (sceneLoad.progress < .9f)
        {
            yield return null;
        }
        sceneLoad.allowSceneActivation = true;
    }
}
