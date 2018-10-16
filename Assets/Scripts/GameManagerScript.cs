/**
 * Authors: Bastien PERROTEAU
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/**
 * Authors : Bastien Perroteau
 */
public class GameManagerScript : MonoBehaviour
{
	[Header("GameState")]
	[SerializeField] private GameObject GameState;

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

	[Header("Menu en jeu")]
	[SerializeField] private GameObject EndMenu;

	// Button Management (Menu / jeu / rejouer)
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

	public void ButtonPlay()
	{
		GameState.GetComponent<GameState>().InGame = true;
	}
	public void ButtonReplay()
	{
		PlayerOne.transform.position = P1Init;
		PlayerTwo.transform.position = P2Init;
		GameState.GetComponent<GameState>().IsKillerOne = false;
		GameState.GetComponent<GameState>().IsKillerTwo = false;
		GameState.GetComponent<GameState>().GumBall.transform.position =
			GameState.GetComponent<GameState>().RandGumball();
		EndMenu.SetActive(false);
		GameState.GetComponent<GameState>().InGame = true;
	}

	//Calcul de distance entre 2 points
	public float DistanceCount(Transform T1, Transform T2)
	{
		return Mathf.Sqrt( Mathf.Pow(T1.position.x - T2.position.x,2) + Mathf.Pow(T1.position.z - T2.position.z,2));
	}
	
	// Initialisation position Player One et Two
	void Start ()
	{
		P1Init = PlayerOne.transform.position;
		P2Init = PlayerTwo.transform.position;
	}

	void Update () {
		//Test si jeu lancé
		if (GameState.GetComponent<GameState>().InGame)
		{
			//Test contact entre sphère
			if (DistanceCount(PlayerOne.transform, PlayerTwo.transform) <= PlayerTwo.transform.localScale.x*1.9)
			{
				if (GameState.GetComponent<GameState>().IsKillerOne)
				{
					GameState.GetComponent<GameState>().InGame = false;
					WinOne.SetActive(true);
					LoseTwo.SetActive(true);
					EndMenu.SetActive(true);
				}
				else if(GameState.GetComponent<GameState>().IsKillerTwo)
				{
					GameState.GetComponent<GameState>().InGame = false;
					WinTwo.SetActive(true);
					LoseOne.SetActive(true);
					EndMenu.SetActive(true);
				}
			}
			//Test contact avec GumBall
			if (DistanceCount(PlayerOne.transform, GameState.GetComponent<GameState>().GumBall.transform) <=
			    GameState.GetComponent<GameState>().GumBall.transform.localScale.x * 1.9)
			{
				GameState.GetComponent<GameState>().IsKillerOne = true;
				GameState.GetComponent<GameState>().Timetokill = GameState.GetComponent<GameState>().TimeKiller;
			}
			else if (DistanceCount(PlayerTwo.transform, GameState.GetComponent<GameState>().GumBall.transform) <=
			           GameState.GetComponent<GameState>().GumBall.transform.localScale.x * 1.9)
			{
				GameState.GetComponent<GameState>().IsKillerTwo = true;
				GameState.GetComponent<GameState>().Timetokill = GameState.GetComponent<GameState>().TimeKiller;
			}
		}
	}
}
