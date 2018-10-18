/**
 * Authors: Bastien PERROTEAU
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManGameEngineScript : MonoBehaviour
{

    public int PlayerOneAgentId, PlayerTwoAgentId = 0;
    private PacManRunner runner;
    private IAgent agentP1, agentP2;
    public PacManGameState gs;
    private float speed = 4;

    [Header("Listes")]
    [SerializeField] private List<Transform> Obstacles;
    [SerializeField] public List<Transform> Doors;

    [Header("Taille Map")]
    [SerializeField] private int x;
    [SerializeField] private int z;

    public int[,] EtatCase;

    [Header("Player/IA One")]
    [SerializeField] public GameObject PlayerOne;
    [SerializeField] private Material MatOne;

    [Header("Player/IA Two")]
    [SerializeField] public GameObject PlayerTwo;
    [SerializeField] private Material MatTwo;

    [Header("Killer")]
    [SerializeField] private Material MatKiller;
    [SerializeField] public float TimeKiller;
    public float Timetokill;
	
	[Header("Affichage Victoire")]
	[SerializeField] private GameObject WinOne;
	[SerializeField] private GameObject WinTwo;

	[Header("Affichage Défaite")]
	[SerializeField] private GameObject LoseOne;
	[SerializeField] private GameObject LoseTwo;
	
	[Header("Panel de fin")]
	[SerializeField] private GameObject EndMenu;
	
    [Header("Speed")]
    [SerializeField] private float WalkSpeed;

    [Header("GumBall")]
    [SerializeField] public GameObject GumBall;

    [Header("Jeu lancé")]
    public bool InGame = false;


    private void Awake()
    {

    }

    // Use this for initialization
    void Start () {
        //A connecter avec l'interface
        //InitializeGame(PlayerOneAgentId,PlayerTwoAgentId);
	}

	// Update is called once per frame
	void Update () {
        if (!InGame)
        {
            return;
        }
        //Run a frame in the runner
        bool[] frameResult = runner.RunFrame();
        gs = runner.GetState();
        PlayerOne.transform.position = gs.GetP1Vector();
        PlayerTwo.transform.position = gs.GetP2Vector();
	    
	    // Test contact entre sphère
	    if (DistanceCount(PlayerOne.transform, PlayerTwo.transform) <= PlayerTwo.transform.localScale.x)
	    {
	        if (gs.GetP1Status())
	        {
	            InGame = false;
	            WinOne.SetActive(true);
	            LoseTwo.SetActive(true);
	            EndMenu.SetActive(true);
	        }
	        else if(gs.GetP2Status())
	        {
	            InGame = false;
	            WinTwo.SetActive(true);
	            LoseOne.SetActive(true);
	            EndMenu.SetActive(true);
	        }
	    }
	    // Test contact avec GumBall
	    if (DistanceCount(PlayerOne.transform, GumBall.transform) <=
	        GumBall.transform.localScale.x * 1.9)
	    {
		    gs.SetGumStatus(false);
		    GumBall.SetActive(false);
		    gs.SetBoolP1(true);
	        Timetokill = TimeKiller;
		    gs.SetPositionGumball(-100,-100,-100);
		    GumBall.transform.position = gs.GetGumVector();
		    PlayerOne.GetComponent<Renderer>().material = MatKiller;
	    }
	    else if (DistanceCount(PlayerTwo.transform, GumBall.transform) <=
	             GumBall.transform.localScale.x * 1.9)
	    {
		    gs.SetGumStatus(false);
		    GumBall.SetActive(false);
	        gs.SetBoolP2(true);
	        Timetokill = TimeKiller;
		    gs.SetPositionGumball(-100,-100,-100);
		    GumBall.transform.position = gs.GetGumVector();
		    PlayerTwo.GetComponent<Renderer>().material = MatKiller;
	    }
		if (Timetokill <= 0)
		{
			Timetokill = 0.1f;
			gs.SetBoolP1(false);
			gs.SetBoolP2(false);
			gs.RandomizeGumball();
			GumBall.transform.position = gs.GetGumVector();
			gs.SetGumStatus(true);
			GumBall.SetActive(true);
		}
		if (gs.GetP1Status() || gs.GetP2Status())
		{
			Timetokill -= Time.deltaTime;
		}
		else
		{
			PlayerOne.GetComponent<Renderer>().material = MatOne;
			PlayerTwo.GetComponent<Renderer>().material = MatTwo;
		}
	    
        if (frameResult[2])//if state is terminal
        {
            InGame = false;
            //TODO Affichage du joueur/agent gagnant et perdant
        }

	}
    // Calcul de distance entre 2 points
    private float DistanceCount(Transform T1, Transform T2)
    {
        return Mathf.Sqrt( Mathf.Pow(T1.position.x - T2.position.x,2) + Mathf.Pow(T1.position.z - T2.position.z,2));
    }

    // Initialisation des agents, du runner et du GameState
    public void InitializeGame(int agent1, int agent2)
    {
        switch (agent1)
        {
            case 0:
                PlayerOne.GetComponent<HumanPlayerScript>().ControlType = 0;
                agentP1 = new HumanPlayerAgent(PlayerOne.GetComponent<HumanPlayerScript>());
                break;
            case 1:
                PlayerOne.GetComponent<HumanPlayerScript>().ControlType = 1;
                agentP1 = new HumanPlayerAgent(PlayerOne.GetComponent<HumanPlayerScript>());
                break;
            case 2:
                agentP1 = new RandomAgent();
                break;
            case 3:
                agentP1 = new RandomRolloutAgent();
                break;
        }
        switch (agent2)
        {
            case 0:
                PlayerTwo.GetComponent<HumanPlayerScript>().ControlType = 0;
                agentP2 = new HumanPlayerAgent(PlayerTwo.GetComponent<HumanPlayerScript>());
                break;
            case 1:
                PlayerTwo.GetComponent<HumanPlayerScript>().ControlType = 1;
                agentP2 = new HumanPlayerAgent(PlayerTwo.GetComponent<HumanPlayerScript>());
                break;
            case 2:
                agentP2 = new RandomAgent();
                break;
            case 3:
                agentP2 = new RandomRolloutAgent();
                break;
        }
        gs = new PacManGameState(x, z, PlayerOne.transform.position, PlayerTwo.transform.position, Obstacles, Doors);
        runner = new PacManRunner(agentP1, agentP2, gs, speed);
        InGame = true;
    }
    private void LaunchGame(){}

}
