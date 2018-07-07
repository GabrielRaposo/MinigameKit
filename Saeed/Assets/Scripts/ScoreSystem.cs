using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour {

    public TextMeshProUGUI scoreRenderer;
    public TextMeshProUGUI hiscoreRenderer;

    int scoreValue, hiscoreValue;
    string textFormat = "000";

    static public ScoreSystem instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start () {
        scoreValue = hiscoreValue = 0;
        UpdateScore();
    }

    public void ResetValue()
    {
        scoreValue = 0;
        UpdateScore();
    }

    public void AddValue() {
        scoreValue++;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreRenderer.text = "Score: " + scoreValue.ToString(textFormat);
        if(scoreValue > hiscoreValue)
        {
            hiscoreValue = scoreValue;
            hiscoreRenderer.text = "Hiscore: " + hiscoreValue.ToString(textFormat);
        }
    }
}
