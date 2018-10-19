/**
 * Authors: Bastien PERROTEAU
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour {
	[Header("Nombre d'agents")]
	[SerializeField] private int NbChoix;
	
	[Header("GameEngine")]
	[SerializeField] private PacManGameEngineScript GameEngine;
	
	[Header("Player attribué")]
	[SerializeField]private int IDPlayer = 1;

	[Header("Texte Affiché")] 
	[SerializeField]private List<GameObject> ListText;

	[Header("Nombre d'itération")] 
	[SerializeField] private Slider Slider;
	[SerializeField] private Text NumberIterationView;

	public void MakeNumberIterations()
	{
		NumberIterationView.text = Slider.value.ToString();
		GameEngine.RandomRNbIteration = Slider.value;
	}
	private void CheckId(int IdAgent)
	{
		foreach (GameObject GO in ListText)
		{
			GO.SetActive(false);
		}
		ListText[IdAgent].SetActive(true);
	}
	private int Nextid(int IdAgent)
	{
		if (IdAgent < NbChoix-1)
		{
			IdAgent++;
		}
		CheckId(IdAgent);
		return IdAgent;
	}

	public void NextButton()
	{
		if (IDPlayer == 1)
		{
			GameEngine.PlayerOneAgentId = Nextid(GameEngine.PlayerOneAgentId);
		}
		else if (IDPlayer == 2)
		{
			GameEngine.PlayerTwoAgentId = Nextid(GameEngine.PlayerTwoAgentId);
		}
	}

	private int Previd(int IdAgent)
	{
		if (IdAgent > 0)
		{
			IdAgent--;
		}
		CheckId(IdAgent);
		return IdAgent;
	}
	public void PreviousButton()
	{
		if (IDPlayer == 1)
		{
			GameEngine.PlayerOneAgentId = Previd(GameEngine.PlayerOneAgentId);
		}
		else if (IDPlayer == 2)
		{
			GameEngine.PlayerTwoAgentId = Previd(GameEngine.PlayerTwoAgentId);
		}
	}
}
