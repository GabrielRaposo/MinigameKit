using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;


public class LayoutTextManager : MonoBehaviour {

    TextMeshProUGUI thisText;
    RectTransform thisRect;

    Vector2 outScreen;
    Vector2 centerScreen;

    private void Awake()
    {
        thisText = GetComponent<TextMeshProUGUI>();
        thisText.text = "";

        thisRect = GetComponent<RectTransform>();

        outScreen = thisRect.position;
        centerScreen = Vector2.zero;

        GetComponent<RectTransform>().transform.position = outScreen;
    }

    public IEnumerator ShowText(string message, float duration)
    {
        Debug.Log("Show text: " + message);

        thisText.text = message;

        yield return new WaitForSeconds(duration);

        thisText.text = "";
    }

    public IEnumerator DownText(string message, float duration)
    {
        thisText.text = message;

        thisRect.DOAnchorPos(centerScreen, duration/2);

        yield return new WaitForSeconds(duration / 2 - duration*0.1f);

        thisRect.DOJumpAnchorPos((new Vector2(centerScreen.x+8, centerScreen.y)), 50.0f, 1, duration/2);

    }

    public void ClearText()
    {
        thisText.text = "";
    }

}
