using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerIcon : MonoBehaviour {

    public TextMeshProUGUI displayTitle;
    public TextMeshProUGUI displayScore;
    public RawImage displayColor;

    public string title { get; private set; }
    public int score { get; private set; }
    public Color color { get; private set; }

    public void Init(string title, int score, Color color)
    {
        this.title = title;
        this.score = score;
        this.color = color;

        if (displayTitle) displayTitle.text = title;
        if (displayScore) displayScore.text = score.ToString();
        if (displayColor) displayColor.color = color;
    }

    public void AddScore(int value = 1)
    {
        score += value;
        if (displayScore) displayScore.text = score.ToString();
    }
}