using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupCardGameState : CardGameState
{
    [SerializeField] int _startingCardNumber = 10;
    [SerializeField] int _numberOfPlayers = 2;

    public override void Enter()
    {
        Debug.Log("Setup: ...Entering");
        Debug.Log("Creating " + _numberOfPlayers + " players.");
        Debug.Log("Creating deck with " + _startingCardNumber + " cards.");
    }

    public override void Exit()
    {
        Debug.Log("Setup: Exiting...");
    }
}
