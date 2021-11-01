using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BoardGame
{
    public class SetupBoardGameState : BoardGameState
    {
        public static event Action CreateBoardBegin;
        public static event Action CreateBoardEnd;

        [SerializeField] CreateBoardGridUI _createBoardGridUI;
        [SerializeField] int _boardSizeX = 9;
        [SerializeField] int _boardSizeY = 9;

        private int _screenSizeX = 1920;
        private int _screenSizeY = 1080;
        private bool _activated = false;
        private bool _createdBoard = false;

        public override void Enter()
        {
            // CANT change state while still in Enter() / Exit() transition!
            // DONT put ChangeState<> here.

            Debug.Log("Setup: ...Entering");
            _activated = false;
            _createdBoard = false;
            StateMachine.Input.PressedConfirm += CreateBoard;
        }

        /*
        public override void Tick()
        {
            if (!_activated && !_createdBoard)
            {
                _activated = true;
                _createdBoard = true;
                StateMachine.ChangeState<PlayerTurnBoardGameState>();
            }
        }
        */
        

        public override void Exit()
        {
            StateMachine.Input.PressedConfirm -= CreateBoard;
            _activated = false;
            _createdBoard = false;
            Debug.Log("Setup: Exiting...");
        }

        private void CreateBoard()
        {
            Debug.Log("Creating board of size: (" + _boardSizeX + "," + _boardSizeY + ")");
            _createBoardGridUI.GenerateGrid(_screenSizeX, _boardSizeY);
            _createdBoard = true;
        }
    }
}
