using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ControllerSetup : MonoBehaviour {

    [Header("Indicators")]
    public ControllerSetupIconManager CSleft;
    public ControllerSetupIconManager CSright;

    [Header("Finish")]
    public Image diamondShape;
    public Button returnButton;
    public TextMeshProUGUI buttonTextOutline;
    public TextMeshProUGUI buttonTextFill;

    int state; //0 - left, 1 - right, 2 - finish

    void OnEnable () {
        ControllerManager.instance.ResetSetup();
        CSleft.ResetIcons();
        CSright.ResetIcons();
        if (returnButton) returnButton.interactable = false;
        SetState(0);
    }
	
	void Update () {
        string input;
        if (ControllerManager.instance != null)
        {
            switch (state)
            {
                case 0:
                    ControllerManager.instance.ControllerSetup();
                    input = ControllerManager.instance.GetLeftController();
                    if(input != string.Empty)
                    {
                        CSleft.SetControllerIcon(input);
                        SetState(1);
                    }
                    break;

                case 1:
                    ControllerManager.instance.ControllerSetup();
                    input = ControllerManager.instance.GetRightController();
                    if (input != string.Empty)
                    {
                        CSright.SetControllerIcon(input);
                        SetState(2);
                    }
                    break;

                default:
                    if (returnButton)
                    {
                        returnButton.interactable = true;
                        EventSystem.current.SetSelectedGameObject(returnButton.gameObject);
                    }
                    break;
            }
        }
    }

    void SetState(int value)
    {
        state = value;

        switch (state)
        {
            case 0:
                CSleft.SetSelection(true);
                CSright.SetSelection(false);
                diamondShape.enabled = false;
                buttonTextOutline.enabled = true;
                buttonTextFill.enabled = false;
                break;

            case 1:
                CSleft.SetSelection(false);
                CSright.SetSelection(true);
                break;

            default:
                CSleft.SetSelection(false);
                CSright.SetSelection(false);
                diamondShape.enabled = true;
                buttonTextOutline.enabled = false;
                buttonTextFill.enabled = true;
                break;
        }
    }
}
