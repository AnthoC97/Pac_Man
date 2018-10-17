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

    public PacManGameState(int x, int z)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < z; j++)
            {
                EtatCase[i, j] = 0;
            }
        }
    }

    public static int Step(PacManGameState p, int action1, int action2)
    {
        return 0;
    }

    public PacManGameState Copy()
    {
        return this;
    }
}
