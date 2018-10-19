/**
 * Authors: Bastien PERROTEAU, Anthony CONTREVILLIERS
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

	public float RandomRNbIteration = 100;

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

		if (Input.GetKeyDown(KeyCode.P))
		{
			InGame = !InGame;
		}
		// Test des booléen
		if (gs.GetGumStatus() == false)
		{
			Timetokill -= Time.deltaTime;
			if (gs.GetP1Status() && GumBall.activeInHierarchy)
			{
				GumBall.SetActive(false);
				Timetokill = TimeKiller;
				gs.SetPositionGumball(-100,-100,-100);
				GumBall.transform.position = gs.GetGumVector();
				PlayerOne.GetComponent<Renderer>().material = MatKiller;
			}
			else if (gs.GetP2Status()&& GumBall.activeInHierarchy)
			{
				GumBall.SetActive(false);
				Timetokill = TimeKiller;
				gs.SetPositionGumball(-100,-100,-100);
				GumBall.transform.position = gs.GetGumVector();
				PlayerTwo.GetComponent<Renderer>().material = MatKiller;
			}
		}
		else
		{
			PlayerOne.GetComponent<Renderer>().material = MatOne;
			PlayerTwo.GetComponent<Renderer>().material = MatTwo;
		}

		if (gs.getP1Winner())
		{
			InGame = false;
			WinOne.SetActive(true);
			LoseTwo.SetActive(true);
			EndMenu.SetActive(true);
		}
		else if (gs.getP2Winner())
		{
			InGame = false;
			WinTwo.SetActive(true);
			LoseOne.SetActive(true);
			EndMenu.SetActive(true);
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
        if (frameResult[2])//if state is terminal
        {
            InGame = false;
            //TODO Affichage du joueur/agent gagnant et perdant
        }
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
                agentP1 = new RandomRolloutAgent(RandomRNbIteration, x, z);
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
                agentP2 = new RandomRolloutAgent(RandomRNbIteration, x, z);
                break;
        }
        gs = new PacManGameState(x, z, PlayerOne.transform.position, PlayerTwo.transform.position, Obstacles, Doors);
        runner = new PacManRunner(agentP1, agentP2, gs, speed);
        InGame = true;
	    GumBall.transform.position = gs.GetGumVector();
    }
    private void LaunchGame(){}

}
