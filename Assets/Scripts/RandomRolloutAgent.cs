/**
 * Authors: Florian CHAMPAUD
 */
using System;

public class RandomRolloutAgent : IAgent
{
    private const int RolloutCount = 100;

    public MovementIntent Act(PacManGameState gs, int playerNumber) {
        int bestActionScore = 0;
        MovementIntent bestAction = 0;
        var movementIntentValues = (MovementIntent[]) Enum.GetValues(typeof(MovementIntent));

        foreach (var action in movementIntentValues) {
            int actionScore = 0;

            for (var i = 0; i > RolloutCount; i++) {
                var gsCopy = new PacManGameState(gs);
                bool[] result;

                var randActionIndex = UnityEngine.Random.Range(0, movementIntentValues.Length);
                MovementIntent randAction = (MovementIntent) movementIntentValues.GetValue(randActionIndex);

                result = PacManGameState.Step(gsCopy, action, randAction, 4);

                while (!result[2]) { // While not terminal state
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
