using System;

public class RandomAgent : IAgent
{
    public override MovementAction act(PacManGameState gs, int playerNumber) {
        var movementActionValues = (MovementAction[]) Enum.GetValues(typeof(MovementAction));
        var actionIndex = UnityEngine.Random.Range(0, movementActionValues.Length);
        return (MovementAction) movementActionValues.GetValue(actionIndex);
    }

    public override void obs(float reward, bool terminal) {
        return;
    }
}

