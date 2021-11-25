using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BoardGame
{
    public class SetupBoardGameBaseState : BoardGameState
    {
        [Header("Board Data")]
        [SerializeField] int _boardSizeX = 9;
        [SerializeField] int _boardSizeY = 9;
        [SerializeField] int _numColors = 3;
        [SerializeField] int _numShapes = 9;

        public int BoardSizeX => _boardSizeX;
        public int BoardSizeY => _boardSizeY;
        public int Colors => _numColors;
        public int Shapes => _numShapes;

        [HideInInspector] public bool _createdBoard = false;


        public override void Enter()
        {
            // CANT change state while still in Enter() / Exit() transition!
            // DONT put ChangeState<> here.

            //Debug.Log("Setup: ...Entering");

            BoardRules();
        }

        
        public override void Tick()
        {
            if (!_createdBoard)
            {
                StateMachine.ChangeState<SetupStateGenerateBoard>();
            }
            if (_createdBoard)
            {
                StateMachine.ChangeState<StartGameState>();
            }
        }
        
        
        public override void Exit()
        {
            //Debug.Log("Setup: Exiting...");
        }

        private void BoardRules()
        {
            _boardSizeX = Mathf.Clamp(_boardSizeX, 0, 9);
            _numShapes = _boardSizeX;

            _numColors = Mathf.Clamp(_numColors, 0, 3);
            _boardSizeY = Mathf.Clamp(_boardSizeY, 0, 9);
        }
    }
}
