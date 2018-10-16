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
	
	/**
 * Returns whether attached to GameObject collides with a wall
 */
	public bool CollidesWithWalls()
	{
		float borderLeft = transform.position.x - transform.localScale.x / 2;
		float borderRight = transform.position.x + transform.localScale.x / 2;
		float borderUp = transform.position.z - transform.localScale.z / 2;
		float borderDown = transform.position.z + transform.localScale.z / 2;

		UnityEngine.Debug.Log(String.Format("borders: {0}; {1}; {2}; {3}. ", borderLeft, borderRight, borderUp, borderDown));

		int[,] walls = GameState.GetComponent<GameState>().EtatCase;

		UnityEngine.Debug.Log(String.Format("walls: {0}, {1}, {2}, {3}",
			walls[(int) borderLeft, (int) borderUp],
			walls[(int) borderLeft, (int) borderDown],
			walls[(int) borderRight, (int) borderUp],
			walls[(int) borderRight, (int) borderDown]));

		UnityEngine.Debug.Log(String.Format("walls: {0}", walls));

		// Works because we are axis aligned and player is not wider than walls
		return walls[(int) borderLeft, (int) borderUp] == 1 ||
		       walls[(int) borderLeft, (int) borderDown] == 1 ||
		       walls[(int) borderRight, (int) borderUp] == 1 ||
		       walls[(int) borderRight, (int) borderDown] == 1;
	}

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
