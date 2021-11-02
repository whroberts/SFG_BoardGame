using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceIdleState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Entering Piece: " + StateMachine.BoardManager.CurrentButton.name + " idle state");

            StateMachine.Input.PressedUp += MoveUp;
            StateMachine.Input.PressedLeft += MoveLeft;
            StateMachine.Input.PressedRight += MoveRight;
            StateMachine.Input.PressedDown += MoveDown;
            StateMachine.Input.PressedJump += MoveJump;
            StateMachine.Input.PressedDiagonalLeft += MoveDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight += MoveDiagonalRight;
        }

        public override void Exit()
        {
            Debug.Log("Exiting Piece: " + StateMachine.BoardManager.CurrentButton.name + " idle state");

            StateMachine.Input.PressedUp -= MoveUp;
            StateMachine.Input.PressedLeft -= MoveLeft;
            StateMachine.Input.PressedRight -= MoveRight;
            StateMachine.Input.PressedDown -= MoveDown;
            StateMachine.Input.PressedJump -= MoveJump;
            StateMachine.Input.PressedDiagonalLeft -= MoveDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight -= MoveDiagonalRight;
        }

        private void MoveUp()
        {
            StateMachine.ChangeState<GamePieceMoveUpState>();
        }

        private void MoveLeft()
        {
            StateMachine.ChangeState<GamePieceMoveLeftState>();
        }

        private void MoveRight()
        {
            StateMachine.ChangeState<GamePieceMoveRightState>();
        }

        private void MoveDown()
        {
            StateMachine.ChangeState<GamePieceMoveDownState>();
        }

        private void MoveJump()
        {
            StateMachine.ChangeState<GamePieceMoveJumpState>();
        }

        private void MoveDiagonalLeft()
        {
            StateMachine.ChangeState<GamePieceMoveDiagonalLeftState>();
        }

        private void MoveDiagonalRight()
        {
            StateMachine.ChangeState<GamePieceMoveDiagonalRightState>();
        }
    }


}
