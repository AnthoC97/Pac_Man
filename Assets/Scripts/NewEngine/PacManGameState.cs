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
    private Transform P1, P2, GumBall;
    private bool P1Killer, P2Killer, GumActive = false;

    //Constructeur
    public PacManGameState(int x, int z, Transform p1, Transform p2, Transform gumball, bool p1k, bool p2k, bool gumact, List<Transform> ListObstacle, List<Transform> ListDoors)
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
    public static int Step(PacManGameState p, MovementIntent action1, MovementIntent action2, float Speed)
    {
        IntentManagement(action1, p.P1, Speed,p);
        IntentManagement(action2, p.P2, Speed,p);
        return 0;
    }
    
    /**
	 * Returns whether passed GameObject collides with a wall
	 */
    private static bool CollidesWithWalls(Transform player, PacManGameState p)
    {
        float borderLeft = player.position.x - player.localScale.x / 2;
        float borderRight = player.position.x + player.localScale.x / 2;
        float borderUp = player.position.z - player.localScale.z / 2;
        float borderDown = player.position.z + player.localScale.z / 2;

        // Works because we are axis aligned and player is not wider than walls
        return p.EtatCase[(int) (borderLeft + .5), (int) (borderUp + .5)] == 1 ||
               p.EtatCase[(int) (borderLeft + .5), (int) (borderDown + .5)] == 1 ||
               p.EtatCase[(int) (borderRight + .5), (int) (borderUp + .5)] == 1 ||
               p.EtatCase[(int) (borderRight + .5), (int) (borderDown + .5)] == 1;
    }
    
    /**
	 * Tries to move a player in a direction, cancelling movement on collision
	 */
    private static void tryMovingInDirection(Transform player, Vector3 direction, float Speed, PacManGameState p) {
        Vector3 prevPosition = player.position;
        player.position += player.rotation * direction * Speed * Time.deltaTime;
        if (CollidesWithWalls(player, p)) {
            player.position = prevPosition;
        }
    }
    
    // Setter mouvement
    private static void IntentManagement(MovementIntent Intent, Transform Player, float Speed, PacManGameState p) {
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
}
