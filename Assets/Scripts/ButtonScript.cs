using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {
	
	public void ButtonQuit()
	{
		Application.Quit();
	}

	public void ButtonPlayMenuSelection()
	{
		SceneManager.LoadScene("SelectionMenu");
	}

	public void ButtonMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void ButtonReplay()
	{
		// A voir Inititaliser position gum en random
		// bool les joueur à false
		// set position joueur
	}
	
}
