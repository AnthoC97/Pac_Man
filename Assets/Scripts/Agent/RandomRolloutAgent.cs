/**
 * Authors: Florian CHAMPAUD
 */
using System;
using UnityEngine;

public class RandomRolloutAgent : IAgent
{
    private const int RolloutCount = 10;
    private const int MaxIterations = 10;
    private PacManGameState gsCopy = new PacManGameState();

    public MovementIntent Act(PacManGameState gs, int playerNumber) {
        int bestActionScore = 0;
        int j;
        MovementIntent bestAction = 0;
        var movementIntentValues = (MovementIntent[]) Enum.GetValues(typeof(MovementIntent));

        foreach (var action in movementIntentValues) {
            int actionScore = 0;

            for (var i = 0; i < RolloutCount; i++) {
                gsCopy.CopyGS(gs);

                var randActionIndex = UnityEngine.Random.Range(0, movementIntentValues.Length);
                MovementIntent randAction = (MovementIntent) movementIntentValues.GetValue(randActionIndex);

                var result = PacManGameState.Step(gsCopy, action, randAction, 4);

                for (j = 0; !result[2] && j < MaxIterations; j++) { // While not terminal state
                    randActionIndex = UnityEngine.Random.Range(0, movementIntentValues.Length);
                    MovementIntent randAction1 = (MovementIntent) movementIntentValues.GetValue(randActionIndex);
                    randActionIndex = UnityEngine.Random.Range(0, movementIntentValues.Length);
                    MovementIntent randAction2 = (MovementIntent) movementIntentValues.GetValue(randActionIndex);

                    result = PacManGameState.Step(gsCopy, randAction1, randAction2, 4);
                }

                actionScore += result[playerNumber] ? 1 : 0;
            }

            if (actionScore > bestActionScore) {
                bestActionScore = actionScore;
                bestAction = action;
            }
        }

        return bestAction;
    }

    public void Obs(float reward, bool terminal) {
    }
}
