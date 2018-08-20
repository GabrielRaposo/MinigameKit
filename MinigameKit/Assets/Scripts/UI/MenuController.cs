using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
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

	[Header("Menus")] [SerializeField] private Menu startUpMenu;
	[SerializeField] private Menu mainMenu;
	[SerializeField] private Menu medleyMenu;
	[SerializeField] Menu freeplayMenu;

	[Space(10)] [Header("Overlays")]
	
	[SerializeField] private Overlay controllerOverlay;
	[SerializeField] private Overlay confirmOverlay;
	[SerializeField] private Overlay medleyOverlay;
	[SerializeField] private Overlay tutorialOverlay;
	
	private bool hasActiveOverlay = false;

    static bool hasSetupControllers;
    static public string FirstScreen = "startup";
	
	void Start ()
	{
        //FirstScreen = "freeplay";
        SwitchMenu(FirstScreen);
    }
	
	void Update ()
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

	public void QuitGame()
	{
		Application.Quit();
	}

	public void SwitchMenu(string menu)
	{
		switch (menu)
		{
			case "startup":
				currentMenu.menuTransform.gameObject.SetActive(false);
				currentMenu = startUpMenu;
				currentMenu.menuTransform.gameObject.SetActive(true);
				eventSystem.SetSelectedGameObject(currentMenu.firstButton);
				break;
			case "main":
				currentMenu.menuTransform.gameObject.SetActive(false);
				currentMenu = mainMenu;
				currentMenu.menuTransform.gameObject.SetActive(true);
				eventSystem.SetSelectedGameObject(currentMenu.firstButton);
                if (!hasSetupControllers)
                {
                    EnableOverlay("controller");
                    hasSetupControllers = true;
                }
                break;
			case "medley":
				currentMenu.menuTransform.gameObject.SetActive(false);
				currentMenu = medleyMenu;
				currentMenu.menuTransform.gameObject.SetActive(true);
				eventSystem.SetSelectedGameObject(currentMenu.firstButton);
                ModeManager.State = ModeManager.GameState.Medley;
				break;
			case "freeplay":
				currentMenu.menuTransform.gameObject.SetActive(false);
				currentMenu = freeplayMenu;
				currentMenu.menuTransform.gameObject.SetActive(true);
				eventSystem.SetSelectedGameObject(currentMenu.firstButton);
                ModeManager.State = ModeManager.GameState.FreePlay;
                break;
		}
	}

	public void EnableOverlay(string overlay)
	{
		switch (overlay)
		{
			case "controller":
				hasActiveOverlay = true;
				currentOverlay = controllerOverlay;
				currentOverlay.overlayTransform.gameObject.SetActive(true);
				lastSelectedObject = eventSystem.currentSelectedGameObject;
				eventSystem.SetSelectedGameObject(currentOverlay.firstButton);
				break;

			case "confirm":
				hasActiveOverlay = true;
				currentOverlay = confirmOverlay;
				currentOverlay.overlayTransform.gameObject.SetActive(true);
				lastSelectedObject = eventSystem.currentSelectedGameObject;
				eventSystem.SetSelectedGameObject(currentOverlay.firstButton);
				break;

			case "medley":
				hasActiveOverlay = true;
				currentOverlay = medleyOverlay;
				currentOverlay.overlayTransform.gameObject.SetActive(true);
				lastSelectedObject = eventSystem.currentSelectedGameObject;
				eventSystem.SetSelectedGameObject(currentOverlay.firstButton);
				break;

			case "tutorial":
				hasActiveOverlay = true;
				currentOverlay = tutorialOverlay;
				currentOverlay.overlayTransform.gameObject.SetActive(true);
				lastSelectedObject = eventSystem.currentSelectedGameObject;
				eventSystem.SetSelectedGameObject(currentOverlay.firstButton);
				break;
		}
	}

	public void DisableOverlay()
	{
		hasActiveOverlay = false;
		eventSystem.SetSelectedGameObject(lastSelectedObject);
		currentOverlay.overlayTransform.gameObject.SetActive(false);
	}
}
