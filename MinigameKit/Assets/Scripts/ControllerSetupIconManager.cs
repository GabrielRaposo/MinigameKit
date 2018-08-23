using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSetupIconManager : MonoBehaviour {
    const float ORIGINAL_Y = 60f;
    const float ORIGINAL_SCALE = .37f;
    const float BIG_SCALE = .55f;

    [Header("UI References")]
    public Image keyboardOutline;
    public Image keyboardFill;

    public Image joystickOutline;
    public Image joystickFill;

    public GameObject inputFlashing;

    public Image diamondShape;

    enum Controller { None, Keyboard, Joystick }
    Controller currentController;

    RectTransform keyboardTransform;
    RectTransform joystickTransform;

    private void Awake()
    {
        keyboardTransform = keyboardOutline.GetComponent<RectTransform>();
        joystickTransform = joystickOutline.GetComponent<RectTransform>();
    }
	
	public void ResetIcons()
    {
        StopAllCoroutines();

        keyboardOutline.fillAmount = 1;
        keyboardFill.fillAmount = 0;

        joystickOutline.enabled = true;
        joystickFill.enabled = false;

        keyboardTransform = keyboardOutline.GetComponent<RectTransform>();
        joystickTransform = joystickOutline.GetComponent<RectTransform>();
        keyboardTransform.localScale = Vector2.one * ORIGINAL_SCALE;
        joystickTransform.localScale = Vector2.one * ORIGINAL_SCALE;

        keyboardTransform.anchoredPosition = Vector2.up   * ORIGINAL_Y;
        joystickTransform.anchoredPosition = Vector2.down * ORIGINAL_Y;

        inputFlashing.SetActive(false);
        currentController = Controller.None;
    }

    public void SetSelection(bool value)
    {
        diamondShape.enabled = value;
        inputFlashing.SetActive(value);
    }

    public void SetControllerIcon(string value)
    {
        if(value[0] == 'K') {
            SetKeyboard(value);
        } else {
            SetJoystick();
        }
        inputFlashing.SetActive(false);
    }

    void SetKeyboard(string value)
    {
        StartCoroutine(FocusIcon(keyboardTransform, joystickTransform));
        keyboardFill.enabled = true;

        if (value[1] == '1') {
            keyboardFill.fillOrigin = 0;
            keyboardOutline.fillOrigin = 1;
            keyboardFill.fillAmount = .5f;
            keyboardOutline.fillAmount = .5f;
        } else {
            keyboardFill.fillOrigin = 1;
            keyboardOutline.fillOrigin = 0;
            keyboardFill.fillAmount = .5f;
            keyboardOutline.fillAmount = .5f;
        }

        joystickOutline.enabled = false;
    }

    void SetJoystick()
    {
        StartCoroutine(FocusIcon(joystickTransform, keyboardTransform));
        joystickOutline.enabled = false;
        keyboardOutline.fillAmount = 0;
        keyboardFill.enabled = false;
        joystickFill.enabled = true;
    }

    IEnumerator FocusIcon(RectTransform iconHighlight, RectTransform iconHide)
    {
        Vector3 moveDirection = (iconHighlight.localPosition.y > 0) ? Vector2.down : Vector2.up;
        float speed = 10f;
        float numOfSteps = ORIGINAL_Y / speed;
        float growthRate = (BIG_SCALE - ORIGINAL_SCALE) / numOfSteps;
        float hideRate = ORIGINAL_SCALE / numOfSteps;

        while (Mathf.Abs(iconHighlight.localPosition.y) > 0)
        {
            iconHighlight.localPosition += moveDirection * speed;
            iconHighlight.localScale += Vector3.one * growthRate;
            iconHide.localScale -= Vector3.one * hideRate;
            yield return new WaitForEndOfFrame();
        }

        iconHighlight.localPosition = Vector3.zero;
        iconHighlight.localScale = Vector3.one * BIG_SCALE;
        iconHide.localScale = Vector3.zero;
    }
}
