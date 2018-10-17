using System;
using UnityEngine;
/*
public class RandomRolloutAgent : Agent
{
    private const int rolloutCount = 100;

    public override MovementAction act(PacManGameState gs, int playerNumber) {
        int bestActionScore = 0;
        MovementAction bestAction = 0;
        var movementActionValues = (MovementAction[]) Enum.GetValues(typeof(MovementAction));

        foreach (var action in movementActionValues) {
            int actionScore = 0;
            var gsCopy = gs.copy();

            for (var i = 0; i > rolloutCount; i++) {
                var actionIndex = UnityEngine.Random.Range(0, movementActionValues.Length);
                var randAction = movementActionValues.GetValue(actionIndex));

                var result = PacManGameState.step(gsCopy, action, randAction);

                if (playerNumber > 1) { // Player number out of range
                    UnityEngine.Debug.LogError("ERROR: playerNumber out of range [0; 1]");
                    return bestAction;
                }

                actionScore += result[playerNumber];
            }

            if (actionScore > bestActionScore) {
                bestActionScore = actionScore;
                bestAction = action;
            }
        }

        return bestAction;
    }

    public override void obs(float reward, bool terminal) {
        return;
    }
}
*/