/**
 * Authors: Bastien PERROTEAU, Florian CHAMPAUD
 */

using System;
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

	public MovementAction IntentP1;
	public MovementAction IntentP2;

	/**
	 * Returns whether attached to GameObject collides with a wall
	 */
	public bool CollidesWithWalls(GameObject player)
	{
		float borderLeft = player.transform.position.x - player.transform.localScale.x / 2;
		float borderRight = player.transform.position.x + player.transform.localScale.x / 2;
		float borderUp = player.transform.position.z - player.transform.localScale.z / 2;
		float borderDown = player.transform.position.z + player.transform.localScale.z / 2;

		UnityEngine.Debug.Log(String.Format("borders: {0}; {1}; {2}; {3}. Position: {4}; {5}.", borderLeft, borderRight, borderUp, borderDown, player.transform.position.x, player.transform.position.z));

		int[,] walls = EtatCase;

		UnityEngine.Debug.Log(String.Format("walls: {0}, {1}, {2}, {3}",
			walls[(int) (borderLeft + .5), (int) (borderUp + .5)],
			walls[(int) (borderLeft + .5), (int) (borderDown + .5)],
			walls[(int) (borderRight + .5), (int) (borderUp + .5)],
			walls[(int) (borderRight + .5), (int) (borderDown + .5)]));

		// Works because we are axis aligned and player is not wider than walls
		return walls[(int) (borderLeft + .5), (int) (borderUp + .5)] == 1 ||
		       walls[(int) (borderLeft + .5), (int) (borderDown + .5)] == 1 ||
		       walls[(int) (borderRight + .5), (int) (borderUp + .5)] == 1 ||
		       walls[(int) (borderRight + .5), (int) (borderDown + .5)] == 1;
	}

	// Setter mouvement
	private void IntentManagement(MovementAction Intent, GameObject Player)
	{
		Vector3 prevPosition = Player.transform.position;
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
		if (CollidesWithWalls(Player)) {
			Player.transform.position = prevPosition;
		}
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

		IntentP1 = 0;
		IntentP2 = 0;
	}
}
