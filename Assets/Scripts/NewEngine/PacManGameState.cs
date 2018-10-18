/**
 * Authors: Bastien PERROTEAU
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacManGameState{
    private int[,] EtatCase;
    private Vector3 P1, P2, GumBall;
    private bool P1Killer, P2Killer, GumActive = false;

    //Constructeur
    public PacManGameState(int x, int z, Vector3 p1, Vector3 p2, List<Transform> ListObstacle, List<Transform> ListDoors)
    {
        // Initialisation à 0 
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                EtatCase[i, j] = 0;
            }
        }
        // Statut des obstacles mis à 1
        foreach (Transform cpt in ListObstacle)
        {
            EtatCase[(int)cpt.position.x,(int)cpt.position.z] = 1;
        }
        // Statut des Portails mis à 2
        foreach (Transform cpt in ListDoors)
        {
            EtatCase[(int)cpt.position.x,(int)cpt.position.z] = 2;
        }
        P1 = p1;
        P2 = p2;
        P1Killer = false;
        P2Killer = false;
        GumActive = false;
        GumBall = this.RandGumball();
    }
        // Ensemble des Getteurs
    // Copie du GameState
    public PacManGameState Copy()
    {
        return this;
    }
    // Récupérer P1
    public Vector3 GetP1Vector()
    {
        return this.P1;
    }
    // Récupérer P2
    public Vector3 GetP2Vector()
    {
        return this.P2;
    }
    // Récupérer Gumball
    public Vector3 GetGumVector()
    {
        return this.GumBall;
    }
    // Récupérer Booléen de P1
    public bool GetP1Status()
    {
        return this.P1Killer;
    }
    // Récupérer Booléen de P1
    public bool GetP2Status()
    {
        return this.P2Killer;
    }
    // Récupérer Booléen de P1
    public bool GetGumStatus()
    {
        return this.GumActive;
    }
    // Récupérer EtatCase
    public int[,] GetEtatCase()
    {
        return this.EtatCase;
    }

    // Récupère Intent et Applique changement de position
    public static bool[] Step(PacManGameState p, MovementIntent action1, MovementIntent action2, float Speed)
    {
        if (p.P1Killer)
        {
            IntentManagement(action1, p.P1, Speed * 1.2f,p);
            IntentManagement(action2, p.P2, Speed,p);
        }
        else if (p.P2Killer)
        {
            IntentManagement(action1, p.P1, Speed,p);
            IntentManagement(action2, p.P2, Speed * 1.2f,p);
        }
        else
        {
            IntentManagement(action1, p.P1, Speed,p);
            IntentManagement(action2, p.P2, Speed,p);
        }
        return new bool[3];
    }
    
    /**
	 * Returns whether passed GameObject collides with a wall
	 */
    private static bool CollidesWithWalls(Vector3 player, PacManGameState p)
    {
        float borderLeft = player.x - 0.35f / 2;
        float borderRight = player.x + 0.35f / 2;
        float borderUp = player.z - 0.35f / 2;
        float borderDown = player.z + 0.35f / 2;

        // Works because we are axis aligned and player is not wider than walls
        return p.EtatCase[(int) (borderLeft + .5), (int) (borderUp + .5)] == 1 ||
               p.EtatCase[(int) (borderLeft + .5), (int) (borderDown + .5)] == 1 ||
               p.EtatCase[(int) (borderRight + .5), (int) (borderUp + .5)] == 1 ||
               p.EtatCase[(int) (borderRight + .5), (int) (borderDown + .5)] == 1;
    }
    
    /**
	 * Tries to move a player in a direction, cancelling movement on collision
	 */
    private static void tryMovingInDirection(Vector3 player, Vector3 direction, float Speed, PacManGameState p) {
        Vector3 prevPosition = player;
        player += direction * Speed * Time.deltaTime;
        if (CollidesWithWalls(player, p)) {
            player = prevPosition;
        }
    }
    
    // Setter mouvement
    private static void IntentManagement(MovementIntent Intent, Vector3 Player, float Speed, PacManGameState p) {
        if ((Intent & MovementIntent.WantToMoveForward) != 0) {
            tryMovingInDirection(Player, Vector3.forward, Speed, p);
        }

        if ((Intent & MovementIntent.WantToMoveBackward) != 0) {
            tryMovingInDirection(Player, Vector3.back, Speed, p);
        }

        if ((Intent & MovementIntent.WantToMoveLeft) != 0) {
            tryMovingInDirection(Player, Vector3.left, Speed, p);
        }
        if ((Intent & MovementIntent.WantToMoveRight) != 0) {
            tryMovingInDirection(Player, Vector3.right, Speed, p);
        }
    }
    // Random Gumball Position
    private Vector3 RandGumball()
    {
        int X = UnityEngine.Random.Range(0, EtatCase.Length);
        int Z = UnityEngine.Random.Range(0, EtatCase.Length);
        while (EtatCase[X,Z] != 0)
        {
            X = UnityEngine.Random.Range(0, EtatCase.Length);
            Z = UnityEngine.Random.Range(0, EtatCase.Length);
        }
        return new Vector3(X,0.3f,Z);
    }

    public void RandomizeGumball()
    {
        this.GumBall = RandGumball();
    }
}
