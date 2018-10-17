/**
 * Authors: Bastien PERROTEAU
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
	[Header("Liste des LevelDesign")]
	[SerializeField] private Transform ListDesign;
	
	[Header("Nombre de LevelDesign")]
	[SerializeField] private int NbChoix;

	private int indLevel = 0;

	private bool Bnext, Bprev = false;
	
	// Button Management (Navigation entre menus)
	public void ButtonQuit()
	{
		Application.Quit();
	}
	public void ButtonMenuSelection()
	{
		SceneManager.LoadScene("SelectionMenu");
	}
	public void ButtonMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void NextLevel()
	{
		if (indLevel < NbChoix - 1)
		{
			indLevel++;
			Bnext = true;
		}
	}

	public void PreviousLevel()
	{
		if (indLevel > 0)
		{
			indLevel--;
			Bprev = true;
		}
	}
	
	public void LoadGameScene()
	{
		if (indLevel == 0)
		{
			SceneManager.LoadScene("GameScene");
		}
		else if (indLevel == 1)
		{
			SceneManager.LoadScene("GameScene");
		}
	}

	private void Update()
	{
		if (Bnext)
		{
			ListDesign.position += new Vector3(0,0,10) * Time.deltaTime;
			if (ListDesign.position.z >= 29*indLevel)
			{
				Bnext = false;
			}
		}
		else if (Bprev)
		{
			ListDesign.position -= new Vector3(0,0,10) * Time.deltaTime;
			if (ListDesign.position.z <= 29*indLevel)
			{
				Bprev = false;
			}
		}
	}
}
