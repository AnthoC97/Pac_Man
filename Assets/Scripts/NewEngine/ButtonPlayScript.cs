/**
 * Authors: Bastien PERROTEAU
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlayScript : MonoBehaviour
{

	[SerializeField] private GameObject PanelStart;
	[SerializeField] private PacManGameState PGS;
	[SerializeField] private PacManGameEngineScript PMGES;
	[SerializeField] private GameObject WinOne;
	[SerializeField] private GameObject WinTwo;
	[SerializeField] private GameObject LoseOne;
	[SerializeField] private GameObject LoseTwo;
	[SerializeField] private GameObject EndMenu;
	
	private Vector3 P1Init, P2Init;
	public void ButtonPlay()
	{
		PanelStart.SetActive(false);
		PGS.RandomizeGumball();
		PGS.SetGumStatus(true);
		PMGES.InGame = true;
	}

	public void ButtonReplay()
	{
		PGS.SetP1(P1Init);
		PGS.SetP2(P2Init);
		PGS.SetBoolP1(false);
		PGS.SetBoolP2(false);
		WinOne.SetActive(false);
		WinTwo.SetActive(false);
		LoseOne.SetActive(false);
		LoseTwo.SetActive(false);
		PGS.RandomizeGumball();
		PGS.SetGumStatus(true);
		EndMenu.SetActive(false);
		PMGES.InGame = true;
	}

	private void Start()
	{
		P1Init = PGS.GetP1Vector();
	}
}
