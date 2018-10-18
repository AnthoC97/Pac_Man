/**
 * Authors: 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAgent {
	// TODO 2 stop conditions: end game, N frames

	MovementIntent Act(PacManGameState gs, int playerNumber);
	void Obs(float reward, bool terminal);
}
