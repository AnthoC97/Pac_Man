using System;

public class RandomAgent : IAgent
{
    public MovementIntent Act(PacManGameState gs, int playerNumber) {
        var movementActionValues = (MovementAction[]) Enum.GetValues(typeof(MovementAction));
        var actionIndex = UnityEngine.Random.Range(0, movementActionValues.Length);
        return (MovementIntent) movementActionValues.GetValue(actionIndex);
    }

    public void Obs(float reward, bool terminal) {
        return;
    }
}

