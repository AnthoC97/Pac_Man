using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

	[Header("Player/IA One")]
	[SerializeField] private GameObject PlayerOne;
	[SerializeField] private GameObject WinOne;
	[SerializeField] private GameObject LoseOne;
	private Vector3 P1Init;

	[Header("Player/IA Two")]
	[SerializeField] private GameObject PlayerTwo;
	[SerializeField] private GameObject WinTwo;
	[SerializeField] private GameObject LoseTwo;
	private Vector3 P2Init;

	[SerializeField] private GameObject EndMenu;

	public bool InGame = false;
	
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

	public void ButtonReplay()
	{
		PlayerOne.transform.position = P1Init;
		PlayerTwo.transform.position = P1Init;
		// A voir Inititaliser position gum en random
		// bool les joueur à false
		// set position joueur
	}
	void Start ()
	{
		P1Init = PlayerOne.transform.position;
		P2Init = PlayerTwo.transform.position;
	}
	
	void Update () {
		if (InGame)
		{
			if (PlayerOne.transform.position.x < PlayerTwo.transform.position.x + PlayerTwo.transform.localScale.x
			    && PlayerOne.transform.position.x > PlayerTwo.transform.position.x - PlayerTwo.transform.localScale.x)
			{
				if (PlayerOne.transform.position.z < PlayerTwo.transform.position.z + PlayerTwo.transform.localScale.z
				    && PlayerOne.transform.position.z > PlayerTwo.transform.position.z - PlayerTwo.transform.localScale.z)
				{
					/*
					if (PlayerOne.GetComponent<IntentScript>().IsKiller)
					{
						InGame = false;
						WinOne.SetActive(true);
						LoseTwo.SetActive(true);
						EndMenu.SetActive(true);
					}
					else if(PlayerTwo.GetComponent<IntentScript>().IsKiller)
					{
						InGame = false;
						WinTwo.SetActive(true);
						LoseOne.SetActive(true);
						EndMenu.SetActive(true);
					}
					*/
				}
			}
		}
	}
}
