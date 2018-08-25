using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialInputLine : MonoBehaviour {

    public TextMeshProUGUI key;
    public TextMeshProUGUI description;

    public void SetValues(TutorialObject.InputKey inputKey, TutorialObject.InputType inputType, string inputText)
    {
        string str = string.Empty;
        switch (inputKey)
        {
            default:
                str += "Stand";
            break;
            case TutorialObject.InputKey.Action:
                str += "Action";
            break;
            case TutorialObject.InputKey.Horizontal:
                str += "Horizontal";
            break;
            case TutorialObject.InputKey.Vertical:
                str += "Vertical";
            break;
        }

        switch (inputType)
        {
            default: break;
            case TutorialObject.InputType.Tap:     str += " (TAP) -";     break;
            case TutorialObject.InputType.Hold:    str += " (HOLD) -";    break;
            case TutorialObject.InputType.Release: str += " (RELEASE) -"; break;
        }

        key.text = str;
        description.text = inputText;
    }
}
