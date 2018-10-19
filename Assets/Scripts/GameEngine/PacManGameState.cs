/**
 * Authors: Bastien PERROTEAU
 */

using System;
using System.Collections.Generic;
using UnityEngine;

public class PacManGameState
{
    private static Vector3[] directions = new[] {Vector3.left, Vector3.right, Vector3.forward, Vector3.back};
    private int[,] EtatCase;
    private Vector3 P1, P2, GumBall;
    private bool P1Killer, P2Killer, GumActive = false;
    private int XSize, ZSize;
    private bool P1Winner, P2Winner, GameEnd = false;

    /**
     * Empty constructor for use with CopyGS method
     */
    public PacManGameState(int xSize, int zSize) {
        this.XSize = xSize;
        this.ZSize = zSize;
        this.EtatCase = new int[xSize, zSize];
    }

    //Constructeur
    public PacManGameState(int x, int z, Vector3 p1, Vector3 p2, List<Transform> ListObstacle, List<Transform> ListDoors)
    {
        EtatCase = new int[x,z];
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

        XSize = x;
        ZSize = z;
        P1 = p1;
        P2 = p2;
        P1Killer = false;
        P2Killer = false;
        GumActive = false;
        GumBall = this.RandGumball();
        this.P1Winner = false;
        this.P2Winner = false;
        this.GameEnd = false;
    }
    // Copie du GameState
    public PacManGameState(PacManGameState PMGS)
    {
        this.EtatCase = new int[PMGS.XSize,PMGS.ZSize];
        // Initialisation de Etat case
        for (int i = 0; i < PMGS.XSize; i++)
        {
            for (int j = 0; j < PMGS.ZSize; j++)
            {
                this.EtatCase[i, j] = PMGS.EtatCase[i, j];
            }
        }

        this.XSize = PMGS.XSize;
        this.ZSize = PMGS.ZSize;
        this.P1 = PMGS.P1;
        this.P2 = PMGS.P2;
        this.P1Killer = PMGS.P1Killer;
        this.P2Killer = PMGS.P2Killer;
        this.GumActive = PMGS.GumActive;
        this.GumBall = PMGS.GumBall;
        this.P1Winner = PMGS.P1Winner;
        this.P2Winner = PMGS.P2Winner;
        this.GameEnd = PMGS.GameEnd;

    }
    // Methode Copie du GameState
    public void CopyGS(PacManGameState PMGS)
    {
        // Initialisation de Etat case
        for (int i = 0; i < PMGS.XSize; i++)
        {
            for (int j = 0; j < PMGS.ZSize; j++)
            {
                this.EtatCase[i, j] = PMGS.EtatCase[i, j];
            }
        }

        this.XSize = PMGS.XSize;
        this.ZSize = PMGS.ZSize;
        this.P1 = PMGS.P1;
        this.P2 = PMGS.P2;
        this.P1Killer = PMGS.P1Killer;
        this.P2Killer = PMGS.P2Killer;
        this.GumActive = PMGS.GumActive;
        this.GumBall = PMGS.GumBall;
        this.P1Winner = PMGS.P1Winner;
        this.P2Winner = PMGS.P2Winner;
        this.GameEnd = PMGS.GameEnd;

    }

        // Ensemble des Setteurs pour Button
    // Set P1
    public void SetP1(Vector3 V3)
    {
        this.P1 = V3;
    }
    // Set P2
    public void SetP2(Vector3 V3)
    {
        this.P2 = V3;
    }
    // Set is killerone
    public void SetBoolP1(bool bl)
    {
        this.P1Killer = bl;
    }
    // Set is killer two
    public void SetBoolP2(bool bl)
    {
        this.P2Killer = bl;
    }
    // Set Position Player
    public void SetPositionForPlayer(int p, Vector3 position) {
        if (p == 0) {
            P1 = position;
        }
        else if (p == 1) {
            P2 = position;
        }
    }
    // Set position Gumball
    public void SetPositionGumball(int gx, int gy, int gz)
    {
        GumBall = new Vector3(gx,gy,gz);
    }
    // Set GumStatus
    public void SetGumStatus(bool bl)
    {
        this.GumActive = bl;
    }
    // Set P1Winner
    public void SetBoolP1Winner(bool bl)
    {
        this.P1Winner = bl;
    }
    // Set P2Winner
    public void SetBoolP2Winner(bool bl)
    {
        this.P2Winner = bl;
    }
    // Set P1Winner
    public void SetBoolEndGame(bool bl)
    {
        this.GameEnd = bl;
    }

        // Ensemble des Getteurs
    //Get de Position du Player
    public Vector3 GetPositionForPlayer(int p) {
        if (p == 0) {
            return P1;
        }
        else if (p == 1) {
            return P2;
        }

        return Vector3.zero;
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

    public int GetXSize() {
        return XSize;
    }

    public int GetZSize() {
        return ZSize;
    }
    public bool getP1Winner()
    {
        return this.P1Winner;
    }
    public bool getP2Winner()
    {
        return this.P2Winner;
    }

    // Récupère Intent et Applique changement de position
    public static bool[] Step(PacManGameState p, MovementIntent action1, MovementIntent action2, float Speed)
    {
        if (p.P1Killer)
        {
            IntentManagement(action1, 0, Speed * 1.2f,p);
            IntentManagement(action2, 1, Speed,p);
        }
        else if (p.P2Killer)
        {
            IntentManagement(action1, 0, Speed,p);
            IntentManagement(action2, 1, Speed * 1.2f,p);
        }
        else
        {
            IntentManagement(action1, 0, Speed,p);
            IntentManagement(action2, 1, Speed,p);
        }
        SearchContactWithGum(p);
        SearchContactBetweenPlayer(p);
        return new bool[3] {p.P1Winner, p.P2Winner, p.GameEnd};
    }

    /**
	 * Returns whether passed GameObject collides with a wall
	 */
    private static bool CollidesWithWalls(Vector3 player, PacManGameState p)
    {
        float borderLeft = player.x - 0.375f;
        float borderRight = player.x + 0.375f;
        float borderUp = player.z - 0.375f;
        float borderDown = player.z + 0.375f;

        if ((int) (borderLeft - .5) < 0 || (int) (borderRight + .5) >= p.XSize ||
            (int) (borderUp - .5) < 0 || (int) (borderDown + .5) >= p.ZSize) {
            return true;
        }

        // Works because we are axis aligned and player is not wider than walls
        return p.EtatCase[(int) (borderLeft + .5), (int) (borderUp + .5)] == 1 ||
               p.EtatCase[(int) (borderLeft + .5), (int) (borderDown + .5)] == 1 ||
               p.EtatCase[(int) (borderRight + .5), (int) (borderUp + .5)] == 1 ||
               p.EtatCase[(int) (borderRight + .5), (int) (borderDown + .5)] == 1;
    }

    /**
	 * Tries to move a player in a direction, cancelling movement on collision
	 */
    private static void tryMovingInDirection(int playerIndex, int direction, float Speed, PacManGameState p) {
        Vector3 newPosition = p.GetPositionForPlayer(playerIndex);
        newPosition += directions[direction] * Speed * Time.deltaTime;

        if (CollidesWithWalls(newPosition, p)) {
            return;
        }

        p.SetPositionForPlayer(playerIndex, newPosition);
    }

    // Setter mouvement
    private static void IntentManagement(MovementIntent Intent, int playerIndex, float Speed, PacManGameState p) {

        if ((Intent & MovementIntent.WantToMoveForward) != 0) {
            tryMovingInDirection(playerIndex, 0, Speed, p);
        }

        if ((Intent & MovementIntent.WantToMoveBackward) != 0) {
            tryMovingInDirection(playerIndex, 1, Speed, p);
        }

        if ((Intent & MovementIntent.WantToMoveLeft) != 0) {
            tryMovingInDirection(playerIndex, 3, Speed, p);
        }
        if ((Intent & MovementIntent.WantToMoveRight) != 0) {
            tryMovingInDirection(playerIndex, 2, Speed, p);
        }
        p.P1 = Portal(p.P1,0.0f,(float)p.XSize - 1.0f,0.0f,(float)p.ZSize - 1.0f);
        p.P2 = Portal(p.P2,0.0f,(float)p.XSize - 1.0f,0.0f,(float)p.ZSize - 1.0f);
    }
    // Passage de Portail
    private static Vector3 Portal(Vector3 Player, float NorthX, float SouthX, float EastZ, float WestZ)
    {
        if (WestZ <= Player.z + (0.375f / 2))
        {
            return new Vector3(Player.x,Player.y,EastZ + (0.375f));
        }
        else if (EastZ >= Player.z - (0.375f / 2))
        {
            return new Vector3(Player.x,Player.y,WestZ - (0.375f));
        }
        else if (SouthX <= Player.x + (0.375f / 2))
        {
            return new Vector3(NorthX + (0.375f),Player.y,Player.z);
        }
        else if (NorthX >= Player.x - (0.375f / 2))
        {
            return new Vector3(SouthX - (0.375f), Player.y, Player.z);
        }
        else return Player;
    }
    // Random Gumball Position
    private Vector3 RandGumball()
    {
        int X = UnityEngine.Random.Range(0, XSize);
        int Z = UnityEngine.Random.Range(0, ZSize);
        while (EtatCase[X,Z] != 0)
        {
            X = UnityEngine.Random.Range(0, XSize);
            Z = UnityEngine.Random.Range(0, ZSize);
        }
        return new Vector3(X,0.3f,Z);
    }
    public void RandomizeGumball()
    {
        this.GumBall = RandGumball();
    }

    private static void SearchContactWithGum(PacManGameState p)
    {
        if (Vector3.Distance(p.P1, p.GumBall) <=
            0.5f * 1.9f)
        {
            p.GumActive = false;
            p.P1Killer = true;
        }
        else if (Vector3.Distance(p.P2, p.GumBall) <=
                 0.5f * 1.9f)
        {
            p.GumActive = false;
            p.P2Killer = true;
        }
    }
    private static void SearchContactBetweenPlayer(PacManGameState p)
    {
        if (Vector3.Distance(p.P1, p.P2) <= 0.70f)
        {
            if (p.P1Killer)
            {
                p.GameEnd = true;
                p.P1Winner = true;
            }
            else if(p.P2Killer)
            {
                p.GameEnd = true;
                p.P2Winner = true;
            }
        }
    }
}
