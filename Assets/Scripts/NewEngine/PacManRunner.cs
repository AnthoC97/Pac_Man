/**
 * Authors: Florian CHAMPAUD
 */

public class PacManRunner{
    private IAgent agent1;
    private IAgent agent2;
    private PacManGameState gs;
    private float speed;

    public PacManRunner(IAgent agent1, IAgent agent2, PacManGameState gs, float speed) {
        this.agent1 = agent1;
        this.agent2 = agent2;
        this.gs = gs;
        this.speed = speed;
    }

    public bool[] RunFrame() {
        var action1 = agent1.Act(gs, 1);
        var action2 = agent2.Act(gs, 2);

        bool[] frameResult = PacManGameState.Step(gs, action1, action2, speed);

        agent1.Obs((frameResult[0] ? 1F : 0F), frameResult[2]);
        agent1.Obs((frameResult[1] ? 1F : 0F), frameResult[2]);

        return frameResult;
    }

    public PacManGameState GetState() {
        return this.gs;
    }
}
