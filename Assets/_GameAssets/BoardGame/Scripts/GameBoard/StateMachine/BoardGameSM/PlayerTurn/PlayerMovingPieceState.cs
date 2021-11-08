using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace BoardGame
{
    public class PlayerMovingPieceState : BoardGameState
    {
        CommandStack _commandStack = new CommandStack();

        public override void Enter()
        {
            Debug.Log("Moving Piece");
            StateMachine.Input.PressedUp += MovePieceUp;
            StateMachine.Input.PressedDiagonalLeft += MovePieceDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight += MovePieceDiagonalRight;
            StateMachine.Input.PressedJump += MovePieceJump;
            StateMachine.Input.PressedConfirm += ConfirmPiece;
            StateMachine.Input.PressedCancel += UndoPieceMove;
        }

        public override void Exit()
        {
            Debug.Log("Moved Piece");
            StateMachine.Input.PressedUp -= MovePieceUp;
            StateMachine.Input.PressedDiagonalLeft -= MovePieceDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight -= MovePieceDiagonalRight;
            StateMachine.Input.PressedJump -= MovePieceJump;
            StateMachine.Input.PressedConfirm -= ConfirmPiece;
            StateMachine.Input.PressedCancel -= UndoPieceMove;
        }

        public void MovePieceUp()
        {
            ICommand moveUpCommand = new MoveUpCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveUpCommand);
        }

        public void MovePieceDiagonalLeft()
        {
            ICommand moveDiagonalLeft = new MoveDiagonalLeftCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalLeft);
        }

        public void MovePieceDiagonalRight()
        {
            ICommand moveDiagonalRight = new MoveDiagonalRightCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalRight);
        }

        public void MovePieceJump()
        {
            ICommand moveJumpCommand = new MoveJumpCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveJumpCommand);
        }

        public void ConfirmPiece()
        {
            if (StateMachine.BoardManager.CurrentButton.GetComponent<PlayerGamePiece>()._moved) {
                foreach (GameObject pieces in StateMachine.BoardManager.PlayerPieces)
                {
                    Button button = pieces.GetComponent<Button>();
                    button.interactable = false;
                }
                StateMachine.ChangeState<EnemyTurnBoardGameState>();
            }
        }

        public void UndoPieceMove()
        {
            PlayerGamePiece piece = StateMachine.BoardManager.CurrentButton.GetComponent<PlayerGamePiece>();
            piece.UndoMove();
        }
    }
}
