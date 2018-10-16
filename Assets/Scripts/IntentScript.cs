using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

	void Start () {

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

	private void FixedUpdate()
	{
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

		intent = 0;
	}
}
