internal class HumanPlayerAgent : Agent
{
    private HumanPlayerScript script;

    public HumanPlayerAgent(HumanPlayerScript script) {
        this.script = script;
    }

    public override MovementAction act(PacManGameState gs, int playerNumber) {
        return script.intent;
    }

    public override void obs(float reward, bool terminal) {
        return;
    }
}
