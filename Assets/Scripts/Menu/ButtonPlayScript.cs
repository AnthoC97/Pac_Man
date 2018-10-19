/**
 * Authors: Bastien PERROTEAU
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayScript : MonoBehaviour
{

	[Header("Panel de début")]
	[SerializeField] private GameObject PanelStart;
	
	[Header("Game Engine")]
	[SerializeField] private PacManGameEngineScript PMGES;
	
	[Header("Affichage Victoire")]
	[SerializeField] private GameObject WinOne;
	[SerializeField] private GameObject WinTwo;
	
	[Header("Affichage Défaite")]
	[SerializeField] private GameObject LoseOne;
	[SerializeField] private GameObject LoseTwo;
	
	[Header("Panel de fin")]
	[SerializeField] private GameObject EndMenu;
	
	private Vector3 P1Init, P2Init;
	
	public void ButtonPlay()
	{
		PMGES.InitializeGame(PMGES.PlayerOneAgentId,PMGES.PlayerTwoAgentId);
		P1Init = PMGES.gs.GetP1Vector();
		P2Init = PMGES.gs.GetP2Vector();
		PanelStart.SetActive(false);
		PMGES.gs.SetGumStatus(true);
	}

	public void ButtonReplay()
	{
		PMGES.gs.SetP1(P1Init);
		PMGES.gs.SetP2(P2Init);
		PMGES.gs.SetBoolEndGame(false);
		PMGES.gs.SetBoolP1Winner(false);
		PMGES.gs.SetBoolP2Winner(false);
		PMGES.gs.SetBoolP1(false);
		PMGES.gs.SetBoolP2(false);
		WinOne.SetActive(false);
		WinTwo.SetActive(false);
		LoseOne.SetActive(false);
		LoseTwo.SetActive(false);
		PMGES.gs.RandomizeGumball();
		PMGES.GumBall.transform.position = PMGES.gs.GetGumVector();
		PMGES.gs.SetGumStatus(true);
		PMGES.GumBall.SetActive(true);
		EndMenu.SetActive(false);
		PMGES.InGame = true;
	}
}
