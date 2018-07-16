using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeManager {
    public enum GameState
    {
        OneByOne,
        Medley
    }

    static public GameState State
    {
        get; private set; 
    }

    static public void SetState(GameState _state)
    {
        Debug.Log("State set to: " + _state);
        State = _state;
    }
}
