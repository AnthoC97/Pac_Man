﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

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
        MovementIntent action1 = agent1.Act(gs,1);
        MovementIntent action2 = agent2.Act(gs,2);

        return PacManGameState.Step(gs, action1, action2, speed);
    }

    public PacManGameState GetState() {
        return this.gs;
    }
}
