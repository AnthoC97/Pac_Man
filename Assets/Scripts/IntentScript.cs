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
	[SerializeField] private GameObject GameState;

	[Header("Index Player One or Two")]
	[SerializeField] public int IndPlayerOT = 1;

	[Header("Indice de contrôle")]
	public int PlayerIndex = 0;

	private MovementAction intent;

	public bool IsKiller = false;

	void Start () {
		//Debug.Assert(level != null, "level != null");
		//walls = level.GetComponent<MappingCounter>().EtatCase;
		//Debug.Assert(walls != null, "walls != null");
	}

	void Update () {
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
		} else if (PlayerIndex == 1) {
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
	}

	private void FixedUpdate() {
		if (IndPlayerOT == 1)
		{
			GameState.GetComponent<GameState>().IntentP1 = intent;
		}
		else if (IndPlayerOT == 2)
		{
			GameState.GetComponent<GameState>().IntentP2 = intent;
		}
		intent = 0;
	}
}
