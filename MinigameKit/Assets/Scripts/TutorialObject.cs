using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute (fileName="Tutorial.asset")]
public class TutorialObject : ScriptableObject  {

    public string minigameName;
    [TextArea(3, 10)] public string gameRules;
    [TextArea(3, 10)] public string controls;
    [Space(10)]
    public Texture2D image;
}
