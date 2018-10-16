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
	[Header("Opposite Character")]
	[SerializeField] private GameObject PlayerO;

	[Header("Speed")]
	[SerializeField] private float WalkSpeed;

	[FormerlySerializedAs("IndJoueur")] [Header("Indice de contrôle")]
	public int PlayerIndex = 0;

	private MovementAction intent;

	private bool IsKiller = false;

	[Header("Mapping")]
	[SerializeField] private GameObject level;

	//private int[,] walls;

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
		Vector3 prevPosition = transform.position;

		if ((intent & MovementAction.WantToMoveForward) != 0) {
			transform.position += transform.rotation * Vector3.forward * WalkSpeed * Time.deltaTime;
		}
		if ((intent & MovementAction.WantToMoveBackward) != 0) {
			transform.position += transform.rotation * Vector3.back * WalkSpeed * Time.deltaTime;
		}
		if ((intent & MovementAction.WantToMoveLeft) != 0) {
			transform.position += transform.rotation * Vector3.left * WalkSpeed * Time.deltaTime;
		}
		if ((intent & MovementAction.WantToMoveRight) != 0) {
			transform.position += transform.rotation * Vector3.right * WalkSpeed * Time.deltaTime;
		}

		if (CollidesWithWalls()) {
			transform.position = prevPosition;
		}

		intent = 0;
	}

	/**
	 * Returns whether attached to GameObject collides with a wall
	 */
	private bool CollidesWithWalls()
	{
		float borderLeft = transform.position.x - transform.localScale.x / 2;
		float borderRight = transform.position.x + transform.localScale.x / 2;
		float borderUp = transform.position.z - transform.localScale.z / 2;
		float borderDown = transform.position.z + transform.localScale.z / 2;

		UnityEngine.Debug.Log(String.Format("borders: {0}; {1}; {2}; {3}. ", borderLeft, borderRight, borderUp, borderDown));

		int[,] walls = level.GetComponent<MappingCounter>().EtatCase;

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
}
