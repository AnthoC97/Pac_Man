using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManGameEngineScript : MonoBehaviour
{

    public int PlayerOneAgentId, PlayerTwoAgentId = 0;
    private PacManRunner runner;
    private IAgent agentP1, agentP2;
    private PacManGameState gs;
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
    [SerializeField] private Material MaTwo;

    [Header("Killer")]
    [SerializeField] private Material MatKiller;
    [SerializeField] public float TimeKiller;
    public float Timetokill;

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
        InitializeGame(PlayerOneAgentId,PlayerTwoAgentId);
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
        if (frameResult[2])//if state is terminal
        {
            InGame = false;
            //TODO Affichage du joueur/agent gagnant et perdant
        }

	}

    private void InitializeGame(int agent1, int agent2)
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

}
