/**
 * Authors: Bastien PERROTEAU
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = System.Diagnostics.Debug;

public enum MovementAction
{
	WantToMoveForward = 1,
	WantToMoveBackward = 2,
	WantToMoveLeft = 4,
	WantToMoveRight = 8,
}

public class IntentScript : MonoBehaviour
{
	[Header("GameState")]
	[SerializeField] private GameState GS;

	[Header("Index Player One or Two")]
	[SerializeField] public int IndPlayerOT = 1;

	[Header("Indice de contrôle")]
	public int PlayerIndex = 0;

	private MovementAction intent;

	public bool IsKiller = false;

	void Update () {
		// Human AZERTY / QWERTY
		if (PlayerIndex == 0) {
			if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) {
				intent = intent | MovementAction.WantToMoveForward;
			}
			if (Input.GetKey(KeyCode.S)) {
				intent = intent | MovementAction.WantToMoveBackward;
			}
			if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) {
				intent = intent | MovementAction.WantToMoveLeft;
			}
			if (Input.GetKey(KeyCode.D)) {
				intent = intent | MovementAction.WantToMoveRight;
			}
		}
		// Human Arrow
		else if (PlayerIndex == 1) {
			if (Input.GetKey(KeyCode.UpArrow)) {
				intent = intent | MovementAction.WantToMoveForward;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				intent = intent | MovementAction.WantToMoveBackward;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				intent = intent | MovementAction.WantToMoveLeft;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				intent = intent | MovementAction.WantToMoveRight;
			}
		}
		// IA Random Agent
		else if (PlayerIndex == 2)
		{
			
		}
		// IA Random Rollout Agent
		else if (PlayerIndex == 3)
		{
			
		}
		// IA Dijkstra Agent / A* Agent 
		else if (PlayerIndex == 4)
		{
			
		}
		// IA  Tabular Q Learning Agent 
		else if (PlayerIndex == 5)
		{
			
		}
	}

	private void LateUpdate() {
		// Si Player 1 on transmet à intentP1
		if (IndPlayerOT == 1)
		{
			GS.IntentP1 = intent;
		}
		// Si Player 2 on transmet à intentP2
		else if (IndPlayerOT == 2)
		{
			GS.IntentP2 = intent;
		}
		intent = 0;
	}
}
