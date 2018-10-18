/**
 * Authors: Florian CHAMPAUD
 */
using System;
using UnityEngine;

public class RandomRolloutAgent : IAgent
{
    private const int rolloutCount = 100;

    public MovementIntent Act(PacManGameState gs, int playerNumber) {
        int bestActionScore = 0;
        MovementIntent bestAction = 0;
        var movementActionValues = (MovementIntent[]) Enum.GetValues(typeof(MovementIntent));

        foreach (var action in movementActionValues) {
            int actionScore = 0;
            var gsCopy = gs.Copy();

            for (var i = 0; i > rolloutCount; i++) {
                var actionIndex = UnityEngine.Random.Range(0, movementActionValues.Length);
                MovementIntent randAction = (MovementIntent) movementActionValues.GetValue(actionIndex);

                var result = PacManGameState.Step(gsCopy, action, randAction,4);

                if (playerNumber > 1) { // Player number out of range
                    UnityEngine.Debug.LogError("ERROR: playerNumber out of range [0; 1]");
                    return bestAction;
                }

                //actionScore += result[playerNumber];
            }

            if (actionScore > bestActionScore) {
                bestActionScore = actionScore;
                bestAction = action;
            }
        }

        return bestAction;
    }

    public void Obs(float reward, bool terminal) {
        return;
    }
}
