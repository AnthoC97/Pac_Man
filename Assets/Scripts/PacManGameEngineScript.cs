using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Agents
{
    randomAgent = 0,
    randomRolloutAgent = 1,
    humanPlayerAgent = 2,
}

public class PacManGameEngineScript : MonoBehaviour {

    private PacManRunner runner;
    private IAgent agentP1, agentP2;
    private PacManGameState gs;
    private bool inGame = false;

    [Header("Taille Map")]
    [SerializeField] private int x;
    [SerializeField] private int z;


    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        //A connecter avec l'interface
        InitializeGame(1,1);
	}
	
	// Update is called once per frame
	void Update () {
        if (!inGame)
        {
            return;
        }

	}

    private void InitializeGame(int agent1, int agent2)
    {
        switch (agent1)
        {
            case 0:
                agentP1 = new RandomAgent();
                break;
            case 1:
                agentP1 = new RandomRolloutAgent();
                break;
            case 2:
                agentP1 = new HumanPlayerAgent();
                break;
        }
        switch (agent2)
        {
            case 0:
                agentP2 = new RandomAgent();
                break;
            case 1:
                agentP2 = new RandomRolloutAgent();
                break;
            case 2:
                agentP2 = new HumanPlayerAgent();
                break;
        }
        gs = new PacManGameState(x, z);
        runner = new PacManRunner(agentP1, agentP2, gs);
        inGame = true;
    }

}
