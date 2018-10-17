/**
 * Authors: Bastien PERROTEAU
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
	[Header("GameState")]
	[SerializeField] private GameState GS;

	[Header("Player/IA One")]
	[SerializeField] private GameObject WinOne;
	[SerializeField] private GameObject LoseOne;
	private Vector3 P1Init;

	[Header("Player/IA Two")]
	[SerializeField] private GameObject WinTwo;
	[SerializeField] private GameObject LoseTwo;
	private Vector3 P2Init;

	[Header("Menu en jeu")]
	[SerializeField] private GameObject EndMenu;
	[SerializeField] private GameObject PanelStart;

	public void ButtonPlay()
	{
		PanelStart.SetActive(false);
		GS.RandomizeGumball();
		GS.InGame = true;
	}
	public void ButtonReplay()
	{
		GS.PlayerOne.transform.position = P1Init;
		GS.PlayerTwo.transform.position = P2Init;
		GS.IsKillerOne = false;
		GS.IsKillerTwo = false;
		WinOne.SetActive(false);
		WinTwo.SetActive(false);
		LoseOne.SetActive(false);
		LoseTwo.SetActive(false);
		GS.RandomizeGumball();
		GS.GumBall.SetActive(true);
		EndMenu.SetActive(false);
		GS.InGame = true;
	}

	// Calcul de distance entre 2 points
	private float DistanceCount(Transform T1, Transform T2)
	{
		return Mathf.Sqrt( Mathf.Pow(T1.position.x - T2.position.x,2) + Mathf.Pow(T1.position.z - T2.position.z,2));
	}
	// Passage de Portail
	private void Portal(GameObject Player, Transform North, Transform South, Transform East, Transform West)
	{
		if (West.position.z <= Player.transform.position.z + (Player.transform.localScale.z / 4))
		{
			Player.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,East.position.z + (Player.transform.localScale.z/2));
		}
		else if (East.position.z >= Player.transform.position.z - (Player.transform.localScale.z / 4))
		{
			Player.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,West.position.z - (Player.transform.localScale.z/2));
		}
		else if (South.position.x <= Player.transform.position.x + (Player.transform.localScale.x / 4))
		{
			Player.transform.position = new Vector3(North.transform.position.x + (Player.transform.localScale.x/2),Player.transform.position.y,Player.transform.position.z);
		}
		else if (North.position.x >= Player.transform.position.x - (Player.transform.localScale.x / 4))
		{
			Player.transform.position = new Vector3(South.transform.position.x - (Player.transform.localScale.x/2),Player.transform.position.y,Player.transform.position.z);
		}
	}

	// Initialisation position Player One et Two
	void Start ()
	{
		P1Init = GS.PlayerOne.transform.position;
		P2Init = GS.PlayerTwo.transform.position;
	}

	void Update () {
		// Test si jeu lancé
		if (GS.InGame)
		{
			// Test contact entre sphère
			if (DistanceCount(GS.PlayerOne.transform, GS.PlayerTwo.transform) <= GS.PlayerTwo.transform.localScale.x)
			{
				if (GS.IsKillerOne)
				{
					GS.InGame = false;
					WinOne.SetActive(true);
					LoseTwo.SetActive(true);
					EndMenu.SetActive(true);
				}
				else if(GS.IsKillerTwo)
				{
					GS.InGame = false;
					WinTwo.SetActive(true);
					LoseOne.SetActive(true);
					EndMenu.SetActive(true);
				}
			}
			// Test contact avec GumBall
			if (DistanceCount(GS.PlayerOne.transform, GS.GumBall.transform) <=
			    GS.GumBall.transform.localScale.x * 1.9)
			{
				GS.IsKillerOne = true;
				GS.Timetokill = GS.TimeKiller;
			}
			else if (DistanceCount(GS.PlayerTwo.transform, GS.GumBall.transform) <=
			         GS.GumBall.transform.localScale.x * 1.9)
			{
				GS.IsKillerTwo = true;
				GS.Timetokill = GS.TimeKiller;
			}
			// Portail
			Portal(GS.PlayerOne, GS.Doors[0], GS.Doors[1], GS.Doors[2], GS.Doors[3]);
			Portal(GS.PlayerTwo, GS.Doors[0], GS.Doors[1], GS.Doors[2], GS.Doors[3]);
		}
	}
}
