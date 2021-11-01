using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class SetupBoardGameState : BoardGameState
    {
        [SerializeField] int _startingCardNumber = 10;
        [SerializeField] int _numberOfPlayers = 2;

        private bool _activated = false;

        public override void Enter()
        {
            Debug.Log("Setup: ...Entering");
            Debug.Log("Creating " + _numberOfPlayers + " players.");
            Debug.Log("Creating deck with " + _startingCardNumber + " cards.");

            // CANT change state while still in Enter() / Exit() transition!
            // DONT put ChangeState<> here.
            _activated = false;
        }

        public override void Tick()
        {
            if (!_activated)
            {
                _activated = true;
                StateMachine.ChangeState<PlayerTurnBoardGameState>();
            }
        }

        public override void Exit()
        {
            _activated = false;
            Debug.Log("Setup: Exiting...");
        }
    }
}
