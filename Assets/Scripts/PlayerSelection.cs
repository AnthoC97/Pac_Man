using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour {
	[Header("Nombre d'agents")]
	[SerializeField] private int NbChoix;
	
	[Header("Player attribué")]
	[SerializeField] private IntentScript Player;

	[Header("Texte Affiché")] [SerializeField]
	private List<GameObject> ListText;

	private void CheckId()
	{
		foreach (GameObject GO in ListText)
		{
			GO.SetActive(false);
		}
		ListText[Player.PlayerIndex].SetActive(true);
	}
	public void Nextid()
	{
		if (Player.PlayerIndex < NbChoix-1)
		{
			Player.PlayerIndex++;
		}
		CheckId();
	}

	public void Previd()
	{
		if (Player.PlayerIndex > 0)
		{
			Player.PlayerIndex--;
		}
		CheckId();
	}
	
	private void Start()
	{
		Player.PlayerIndex = 0;
	}
}
