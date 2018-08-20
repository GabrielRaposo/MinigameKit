using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : ButtonManager
{
	enum buttonType {openMenu, openOverlay};

	[SerializeField] buttonType ButtonType;

	public override void Press()
	{
		if(ButtonType == buttonType.openMenu)
		{
			 return;  //Implemente a 'ChangeMenu()' on the MenuManager and then call it here.
		}

		if(ButtonType == buttonType.openOverlay)
		{
			 return;  //Implemente a 'ToggleOverlay(Transform overlay)' on the MenuManager and then call it here.
		} 
	}
}
