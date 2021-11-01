using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class SetupBoardGameState : BoardGameState
    {
        [SerializeField] int _boardSizeX = 9;
        [SerializeField] int _boardSizeY = 9;

        private bool _activated = false;

        public override void Enter()
        {
            Debug.Log("Setup: ...Entering");
            Debug.Log("Creating board of size: (" + _boardSizeX + "," + _boardSizeY + ")");

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
