/**
 * Authors: Bastien PERROTEAU
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MovementIntent
{
    WantToMoveForward = 1,
    WantToMoveBackward = 2,
    WantToMoveLeft = 4,
    WantToMoveRight = 8,
}
public enum Agents
{
    randomAgent = 0,
    randomRolloutAgent = 1,
    humanPlayerAgent = 2,
}
public class Enums {

}
