using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class TutorialScreen : MonoBehaviour
{
    [Header("Main components")]
	public TextMeshProUGUI title;
    public RawImage image;

    [Header("Rules References")]
    public RectTransform boxRules;
    public Button titleRules;
    public GameObject arrowRules;
    public TextMeshProUGUI displayRules;

    [Header("Controls References")]
    public RectTransform boxControls;
    public Button titleControls;
    public GameObject arrowControls;

    public GameObject inputLinePrefab;
    public Transform inputLineParent;


    [Header("Values")]
    public Color colorFront;
    public Color colorBack;

    string codename;
    string gameTitle;
    string rulesText;
    Texture gameImage;

    enum State { Rules, Controls }
    State state;
    Animator _animator;
    List<GameObject> inputLines;

    bool allowInput;

    private void OnEnable()
    {
        state = State.Rules;
        SetState();
    }

    private void Update()
    {
        if (!allowInput) return;

        float verticalInput = Input.GetAxisRaw("Vertical");
        if (verticalInput > 0 && state != State.Controls) {
            SwitchState(1);
        } else 
        if (verticalInput < 0 && state != State.Rules) {
            SwitchState(0);
        }
    }

    public void SwitchState(int index)
    {
        if (index == 0) {
            state = State.Rules;
        } else {
            state = State.Controls;
        }

        GetComponent<Animator>().SetTrigger("Swap");
        allowInput = false;
    }

    void SetState()
    {
        switch (state)
        {
            case State.Rules:
                HighlightVisual(true, boxRules, titleRules, arrowRules);
                HighlightVisual(false, boxControls, titleControls, arrowControls);
                SetDrawOrder(boxRules.transform, boxControls.transform);
            break;

            case State.Controls:
                HighlightVisual(true, boxControls, titleControls, arrowControls);
                HighlightVisual(false, boxRules, titleRules, arrowRules);
                SetDrawOrder(boxControls.transform, boxRules.transform);
            break;
        }
        allowInput = true;
    } 

    void HighlightVisual(bool value, RectTransform box, Button title, GameObject arrow)
    {
        Color color = value ? colorFront : colorBack;
        box.GetComponent<RawImage>().color = color;
        title.GetComponent<RawImage>().color = color;
        arrow.SetActive(!value);
        title.interactable = !value;
    }

    void SetDrawOrder(Transform front, Transform back)
    {
        back.SetAsLastSibling();
        front.SetAsLastSibling();
    }  

    public void GetInfo(string codename, string gameTitle, string rulesText, TutorialObject.InputTab[] controls, Texture gameImage)
	{
        this.codename = codename;
        this.gameTitle = gameTitle;
        this.rulesText = rulesText;
        this.gameImage = gameImage;

        title.text = gameTitle;
        image.texture = gameImage;

        displayRules.text = rulesText;
        CreateInputLines(controls);

        gameObject.SetActive(true);
    }

    void CreateInputLines(TutorialObject.InputTab[] controls)
    {
        foreach (Transform child in inputLineParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < controls.Length; i++)
        {
            GameObject line = Instantiate(inputLinePrefab, inputLineParent);
            line.GetComponent<TutorialInputLine>().SetValues(
                controls[i].inputKey,
                controls[i].inputType,
                controls[i].text
            );
            line.SetActive(true);
        }
    }

    public void StartMinigame()
    {
        if(codename != string.Empty)
        {
            SceneManager.LoadScene("Minigames/" + codename + "/" + codename);
        }
    }
}
