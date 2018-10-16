using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Flags]
public enum MouvementAction
{
	WantToMoveForward = 1,
	WantToMoveBackward = 2,
	WantToMoveLeft = 4,
	WantToMoveRight = 8,
}
public class IntentScript : MonoBehaviour
{

	[SerializeField] private Transform PlayerT;

	[SerializeField] private float WalkSpeed;
	public int IndJoueur;

	private MouvementAction intent;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (IndJoueur == 0)
		{
			if (Input.GetKeyDown(KeyCode.Z))
			{
				intent = intent | MouvementAction.WantToMoveForward;
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
				intent = intent | MouvementAction.WantToMoveBackward;
			}
			if (Input.GetKeyDown(KeyCode.Q))
			{
				intent = intent | MouvementAction.WantToMoveLeft;
			}if (Input.GetKeyDown(KeyCode.D))
			{
				intent = intent | MouvementAction.WantToMoveRight;
			}
		}
		else if (IndJoueur == 1)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				intent = intent | MouvementAction.WantToMoveForward;
			}
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				intent = intent | MouvementAction.WantToMoveBackward;
			}
			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				intent = intent | MouvementAction.WantToMoveLeft;
			}if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				intent = intent | MouvementAction.WantToMoveRight;
			}
		}
	}

	private void FixedUpdate()
	{
		if ((intent & MouvementAction.WantToMoveForward) == MouvementAction.WantToMoveForward)
		{
			PlayerT.position += PlayerT.rotation * Vector3.forward * WalkSpeed * Time.deltaTime;
		}
		if ((intent & MouvementAction.WantToMoveBackward) == MouvementAction.WantToMoveBackward)
		{
			PlayerT.position += PlayerT.rotation * Vector3.back * WalkSpeed * Time.deltaTime;
		}
		if ((intent & MouvementAction.WantToMoveLeft) == MouvementAction.WantToMoveLeft)
		{
			PlayerT.position += PlayerT.rotation * Vector3.left * WalkSpeed * Time.deltaTime;
		}
		if ((intent & MouvementAction.WantToMoveRight) == MouvementAction.WantToMoveRight)
		{
			PlayerT.position += PlayerT.rotation * Vector3.right * WalkSpeed * Time.deltaTime;
		}
	}
}
