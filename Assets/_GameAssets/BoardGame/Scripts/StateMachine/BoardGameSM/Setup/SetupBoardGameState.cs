using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class SetupBoardGameState : BoardGameState
    {
        [SerializeField] GenerateBoardUI _generateBoardUI = null;
        [SerializeField] int _boardSizeX = 9;
        [SerializeField] int _boardSizeY = 9;

        private bool _createdBoard = false;

        public override void Enter()
        {
            // CANT change state while still in Enter() / Exit() transition!
            // DONT put ChangeState<> here.

            Debug.Log("Setup: ...Entering");
            CreateBoard();
            _createdBoard = false;
        }

        
        public override void Tick()
        {
            if (!_createdBoard)
            {
                _createdBoard = true;
                StateMachine.ChangeState<PlayerTurnBoardGameState>();
            }
        }
        
        
        public override void Exit()
        {
            _createdBoard = false;
            Debug.Log("Setup: Exiting...");
        }

        private void CreateBoard()
        {
            Debug.Log("Creating board of size: (" + _boardSizeX + "," + _boardSizeY + ")");
            _generateBoardUI.GenerateGrid(_boardSizeX, _boardSizeY);
            _createdBoard = true;
        }
    }
}
