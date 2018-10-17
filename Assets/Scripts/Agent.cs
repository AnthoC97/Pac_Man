using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Agent
{
	// TODO 2 stop conditions: end game, N frames

	public abstract MovementAction act(PacManGameState gs, int playerNumber);
	public abstract void obs(float reward, bool terminal);
}

