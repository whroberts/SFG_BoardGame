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
        [SerializeField] int _numColors = 3;
        [SerializeField] int _numShapes = 9;

        private bool _createdBoard = false;

        public override void Enter()
        {
            // CANT change state while still in Enter() / Exit() transition!
            // DONT put ChangeState<> here.

            Debug.Log("Setup: ...Entering");

            BoardRules();
            CreateBoard();
        }

        
        public override void Tick()
        {
            if (_createdBoard)
            {
                _createdBoard = false;
                StateMachine.ChangeState<PlayerTurnBoardGameState>();
            }
        }
        
        
        public override void Exit()
        {
            _createdBoard = false;
            Debug.Log("Setup: Exiting...");
        }

        private void BoardRules()
        {
            _boardSizeX = Mathf.Clamp(_boardSizeX, 0, 9);
            _numShapes = _boardSizeX;

            _numColors = Mathf.Clamp(_numColors, 0, 3);
            _boardSizeY = Mathf.Clamp(_boardSizeY, 0, 9);
        }

        private void CreateBoard()
        {
            Debug.Log("Creating board of size: (" + _boardSizeX + "," + _boardSizeY + ")");
            _generateBoardUI.GenerateGrid(_boardSizeX, _boardSizeY);
            _generateBoardUI.GenerateGamePieces(_numShapes, _numColors);
            _createdBoard = true;
        }
    }
}
