using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute (fileName="Tutorial.asset")]
public class TutorialObject : ScriptableObject  {
    public enum InputKey
    {
        None, 
        Action, 
        Horizontal,
        Vertical
    }

    public enum InputType
    {
        None,
        Tap,
        Hold,
        Release
    }

    [System.Serializable]
    public struct InputTab
    {
        public InputKey inputKey;
        public InputType inputType;
        [TextArea(1, 1)] public string text;
    }

    public string minigameName;
    public string codename;
    [TextArea(3, 10)] public string gameRules;
    public InputTab[] controls;
    [Space(10)] public Texture2D image;
    [Space(10)] public Texture2D icon;
}
