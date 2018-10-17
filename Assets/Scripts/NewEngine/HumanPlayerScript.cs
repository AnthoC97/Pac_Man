/**
 * Authors: Bastien PERROTEAU
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerScript : MonoBehaviour {

	[Header("GameState")]
	[SerializeField] private GameState GS;

	[Header("Index Player One or Two")]
	[SerializeField] public int PlayerIndex = 1;

	[Header("Indice de contrôle")]
	public int ControlType = 0;

	private MovementAction intent;


	void Update () {
		// Human AZERTY / QWERTY
		if (ControlType == 0) {
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
		else if (ControlType == 1) {
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

	private void LateUpdate() {
		// Si Player 1 on transmet à intentP1
		if (PlayerIndex == 1)
		{
			GS.IntentP1 = intent;
		}
		// Si Player 2 on transmet à intentP2
		else if (PlayerIndex == 2)
		{
			GS.IntentP2 = intent;
		}
		intent = 0;
	}
}