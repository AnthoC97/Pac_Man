/**
 * Authors: Florian CHAMPAUD
 */

internal class HumanPlayerAgent : IAgent
{
    private HumanPlayerScript script;

    public HumanPlayerAgent(HumanPlayerScript script) {
        this.script = script;
    }

    public MovementIntent Act(PacManGameState gs, int playerNumber) {
        return gs.IntentP1;
    }

    public void Obs(float reward, bool terminal) {
        return;
    }
}
