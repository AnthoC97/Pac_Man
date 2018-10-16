using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Authors : Bastien Perroteau
 */
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
	[SerializeField]private Material MatOne;
	
	[Header("Player/IA Two")]
	[SerializeField] private GameObject PlayerTwo;
	public bool IsKillerTwo = false;
	[SerializeField]private Material MaTwo;
	
	[Header("Killer")]
	[SerializeField]private Material MatKiller;
	[SerializeField]public float TimeKiller;
	public float Timetokill;
	
	[Header("Speed")]
	[SerializeField] private float WalkSpeed;

	[Header("GumBall")] 
	[SerializeField] public GameObject GumBall;
	
	[Header("Jeu lancé")]
	public bool InGame = false;
	
	public MovementAction IntentP1;
	public MovementAction IntentP2;
	
	//Random Gumball Position
	public Vector3 RandGumball()
	{
		int X = Random.Range(0, 18);
		int Z = Random.Range(0, 18);
		while (EtatCase[X,Z] != 0)
		{
			X = Random.Range(0, 18);
			Z = Random.Range(0, 18);
		}
		return new Vector3(X,0.3f,Z);
	}
	
	// Setter mouvement
	private void IntentManagement(MovementAction Intent, GameObject Player)
	{
		Vector3 prevPosition = transform.position;
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
		if (Player.GetComponent<IntentScript>().CollidesWithWalls()) {
			transform.position = prevPosition;
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
		//Set Gumball Position
		GumBall.transform.position = RandGumball();
	}

	private void Update()
	{
		if (Timetokill <= 0)
		{
			Timetokill = 0.1f;
			IsKillerOne = false;
			IsKillerTwo = false;
			GumBall.transform.position = RandGumball();
			GumBall.SetActive(true);
		}
		if (IsKillerOne)
		{
			GumBall.transform.position = new Vector3(25,-25,25);
			GumBall.SetActive(false);
			Timetokill -= Time.deltaTime;
			PlayerOne.GetComponent<Renderer>().material = MatKiller;
		}
		else if (IsKillerTwo)
		{
			GumBall.transform.position = new Vector3(25,-25,25);
			GumBall.SetActive(false);
			Timetokill -= Time.deltaTime;
			PlayerTwo.GetComponent<Renderer>().material = MatKiller;
		}
		else
		{
			PlayerOne.GetComponent<Renderer>().material = MatOne;
			PlayerTwo.GetComponent<Renderer>().material = MaTwo;
		}
	}

	void FixedUpdate()
	{
		GumBall.transform.Rotate(new Vector3(0,25,0) * Time.deltaTime);
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
