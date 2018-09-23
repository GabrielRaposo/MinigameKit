using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MedleyController : MonoBehaviour
{
    [Header("References")]

    [SerializeField] EventSystem eventSystem;
    GameObject lastSelectedObject;

    [System.Serializable]
    private struct Menu
    {
        public Transform menuTransform;
        public GameObject firstButton;
        public string previousMenu;
    }

    [System.Serializable]
    private struct Overlay
    {
        public Transform overlayTransform;
        public GameObject firstButton;
    }

    [SerializeField] Menu currentMenu;
    [SerializeField] Overlay currentOverlay;

    [Header("Menus")]
    [SerializeField] private Menu titleMenu;
    [SerializeField] private Menu mainMenu;

    [Space(10)]
    [Header("Overlays")]

    [SerializeField] private Overlay infoOverlay;
    [SerializeField] private Overlay settingsOverlay;
    [SerializeField] private Overlay tutorialOverlay;

    private bool hasActiveOverlay = false;

    static bool hasSetupControllers;
    static public string FirstScreen = "title";

    private void Start()
    {
        //FirstScreen = "freeplay";
        SwitchMenu(FirstScreen);
        ModeManager.State = ModeManager.GameState.Medley;
    }

    void Update()
    {
        if (!hasActiveOverlay && eventSystem.currentSelectedGameObject == null)
        {
            //temp pra lidar com mouse chato
            eventSystem.SetSelectedGameObject(currentMenu.firstButton);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (hasActiveOverlay) DisableOverlay();
            else SwitchMenu(currentMenu.previousMenu);
        }
    }

    public void SwitchMenu(string menu)
    {
        currentMenu.menuTransform.gameObject.SetActive(false);
        switch (menu)
        {
            case "title":
                currentMenu = titleMenu;
                break;
            case "main":
                currentMenu = mainMenu;
                FirstScreen = "main";
                break;
            default:
                return;
        }
        eventSystem.SetSelectedGameObject(currentMenu.firstButton);
        currentMenu.menuTransform.gameObject.SetActive(true);
        eventSystem.SetSelectedGameObject(currentMenu.firstButton);
    }

    public void EnableOverlay(string overlay)
    {
        switch (overlay)
        {
            case "info":
                currentOverlay = infoOverlay;
                break;

            case "settings":
                currentOverlay = settingsOverlay;
                break;

            case "tutorial":
                currentOverlay = tutorialOverlay;
                break;

            default:
                Debug.Log("Titulo de overlay incorreto.");
                return;
        }
        hasActiveOverlay = true;
        currentOverlay.overlayTransform.gameObject.SetActive(true);
        lastSelectedObject = eventSystem.currentSelectedGameObject;
        eventSystem.SetSelectedGameObject(currentOverlay.firstButton);
    }

    public void DisableOverlay()
    {
        hasActiveOverlay = false;
        eventSystem.SetSelectedGameObject(lastSelectedObject);
        currentOverlay.overlayTransform.gameObject.SetActive(false);
    }

    public void CallScene(string scene)
    {
        switch (scene)
        {
            case "main menu":
                FirstScreen = "title";
                ModeManager.State = ModeManager.GameState.FreePlay;
                MenuController.FirstScreen = "main";
                StartCoroutine(ModeManager.TransitionToMenu());
                break;

            default:
                Debug.Log("Titulo de overlay incorreto.");
                return;
        }
    }
}
