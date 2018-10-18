/**
 * Authors: Bastien PERROTEAU
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayerScript : MonoBehaviour {

	[Header("Index Player One or Two")]
	[SerializeField] public int PlayerIndex = 1;

	[Header("Indice de contrôle")]
	public int ControlType = 0;

	public MovementIntent Intent { get; private set; }


	void Update () {
		Intent = 0;
		// Human AZERTY / QWERTY
		if (ControlType == 0) {
			if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) {
				Intent = Intent | MovementIntent.WantToMoveForward;
			}
			if (Input.GetKey(KeyCode.S)) {
				Intent = Intent | MovementIntent.WantToMoveBackward;
			}
			if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A)) {
				Intent = Intent | MovementIntent.WantToMoveLeft;
			}
			if (Input.GetKey(KeyCode.D)) {
				Intent = Intent | MovementIntent.WantToMoveRight;
			}
		}
		// Human Arrow
		else if (ControlType == 1) {
			if (Input.GetKey(KeyCode.UpArrow)) {
				Intent = Intent | MovementIntent.WantToMoveForward;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				Intent = Intent | MovementIntent.WantToMoveBackward;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				Intent = Intent | MovementIntent.WantToMoveLeft;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				Intent = Intent | MovementIntent.WantToMoveRight;
			}
		}
	}
}