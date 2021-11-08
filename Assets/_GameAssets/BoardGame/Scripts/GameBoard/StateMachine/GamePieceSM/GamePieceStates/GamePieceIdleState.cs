using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoardGame
{
    public class GamePieceIdleState : GamePieceState
    {
        [SerializeField] TMP_Text _pieceControls = null;

        public override void Enter()
        {
            Debug.Log("Entering Piece: " + StateMachine.BoardManager.PlayerCurrentButton.name + " idle state");
            /*
            StateMachine.Input.PressedUp += MoveUp;
            StateMachine.Input.PressedLeft += MoveLeft;
            StateMachine.Input.PressedRight += MoveRight;
            StateMachine.Input.PressedDown += MoveDown;
            */

            StateMachine.Input.PressedJump += MoveJump;
            StateMachine.Input.PressedDiagonalLeft += MoveDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight += MoveDiagonalRight;
            StateMachine.Input.PressedCancel += Cancel;

            _pieceControls.gameObject.SetActive(true);
            StateMachine.BoardManager.PlayerCurrentButton.onClick.RemoveAllListeners();

        }

        public override void Exit()
        {
            Debug.Log(StateMachine.BoardManager.PlayerCurrentButton);

            if (StateMachine.BoardManager.PlayerCurrentButton != null)
            {
                Debug.Log("Exiting Piece: " + StateMachine.BoardManager.PlayerCurrentButton.name + " idle state");
            }
            else
            {
                Debug.Log("Changing Piece");
            }

            /*
            StateMachine.Input.PressedUp -= MoveUp;
            StateMachine.Input.PressedLeft -= MoveLeft;
            StateMachine.Input.PressedRight -= MoveRight;
            StateMachine.Input.PressedDown -= MoveDown;
            */

            StateMachine.Input.PressedJump -= MoveJump;
            StateMachine.Input.PressedDiagonalLeft -= MoveDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight -= MoveDiagonalRight;
            StateMachine.Input.PressedCancel -= Cancel;

            _pieceControls.gameObject.SetActive(false);
        }

        private void Cancel()
        {
            //StateMachine.BoardManager.SetCurrentButton(null);
            StateMachine.ChangeState<ChooseGamePieceState>();
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

        // Invalid Movement
        /*
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
        */
    }
}
