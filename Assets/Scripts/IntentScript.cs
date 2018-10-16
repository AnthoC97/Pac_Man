using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouvementAction
{
	WantToMoveForward = 1,
	WantToMoveBackward = 2,
	WantToMoveLeft = 4,
	WantToMoveRight = 8,
}
public class IntentScript : MonoBehaviour
{

	
	[Header("PlayerCharacter")]
	[SerializeField] private Transform PlayerT;

	[Header("Opposite Character")] 
	[SerializeField] private Transform PlayerO;
	private bool OppositeKiller = false;
	
	[Header("Speed")]
	[SerializeField] private float WalkSpeed;
	
	[Header("Indice de contrôle")]
	public int IndJoueur = 0;

	private MouvementAction intent;

	private bool IsKiller = false;

	void Start () {
		
	}
	
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
