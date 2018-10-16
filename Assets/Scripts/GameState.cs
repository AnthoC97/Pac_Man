using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	[Header("Listes")]
	[SerializeField] private List<Transform> Obstacles;
	[SerializeField] private List<Transform> Doors;
	
	[Header("Taille Map")]
	[SerializeField] private int x;
	[SerializeField] private int z;
	
	public int[,] EtatCase;
	
	[Header("Player/IA One")]
	[SerializeField] private GameObject PlayerOne;
	public bool IsKillerOne = false;
	
	[Header("Player/IA Two")]
	[SerializeField] private GameObject PlayerTwo;
	public bool IsKillerTwo = false;
	
	[Header("Speed")]
	[SerializeField] private float WalkSpeed;
	
	[Header("Jeu lancé")]
	public bool InGame = false;
	
	private MovementAction IntentP1;
	private MovementAction IntentP2;
	
	// Setter mouvement
	private void IntentManagement(MovementAction Intent, GameObject Player)
	{
		if ((Intent & MovementAction.WantToMoveForward) != 0) {
			Player.transform.position += Player.transform.rotation * Vector3.forward * WalkSpeed * Time.deltaTime;
		}
		if ((Intent & MovementAction.WantToMoveBackward) != 0) {
			Player.transform.position += Player.transform.rotation * Vector3.back * WalkSpeed * Time.deltaTime;
		}
		if ((Intent & MovementAction.WantToMoveLeft) != 0) {
			Player.transform.position += Player.transform.rotation * Vector3.left * WalkSpeed * Time.deltaTime;
		}
		if ((Intent & MovementAction.WantToMoveRight) != 0) {
			Player.transform.position += Player.transform.rotation * Vector3.right * WalkSpeed * Time.deltaTime;
		}

		Intent = 0;
	}
	
	void Start ()
	{
		//Initialisation du statut à 0 (passage possible)
		EtatCase = new int[x,z];
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < z; j++)
			{
				EtatCase[i,j] = 0;
			}
		}
		//Statut des obstacles mis à 1
		foreach (Transform cpt in Obstacles)
		{
			EtatCase[(int)cpt.position.x,(int)cpt.position.z] = 1;
		}
		//Statut des Portails mis à 2
		foreach (Transform cpt in Doors)
		{
			EtatCase[(int)cpt.position.x,(int)cpt.position.z] = 2;
		}
	}
	
	void FixedUpdate()
	{
		//Test si jeu lancé
		if (InGame)
		{
			IntentManagement(IntentP1,PlayerOne);
			IntentManagement(IntentP2,PlayerTwo);
		}
	}
}
