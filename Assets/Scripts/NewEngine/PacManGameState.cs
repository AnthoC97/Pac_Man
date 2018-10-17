using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementIntent
{
    WantToMoveForward = 1,
    WantToMoveBackward = 2,
    WantToMoveLeft = 4,
    WantToMoveRight = 8,
}

public class PacManGameState{
    private int[,] EtatCase;
    private Vector3 P1, P2, GumBall;
    private bool P1Killer, P2Killer, GumActive = false;

    //Constructeur
    public PacManGameState(int x, int z, Vector3 p1, Vector3 p2, Vector3 gumball, bool p1k, bool p2k, bool gumact, List<Transform> ListObstacle, List<Transform> ListDoors)
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
        GumBall = gumball;
        P1Killer = p1k;
        P2Killer = p2k;
        GumActive = gumact;
    }
    // Copie du GameState
    public PacManGameState Copy()
    {
        return this;
    }

    // Récupère Intent et Applique changement de position
    public static int Step(PacManGameState p, MovementIntent action1, MovementIntent action2)
    {
        return 0;
    }
}
