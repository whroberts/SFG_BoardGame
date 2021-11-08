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
        [SerializeField] TMP_Text _chosenPieceText = null;
        [SerializeField] TMP_Text _pieceControlsText = null;

        CommandStack _commandStack = new CommandStack();

        public override void Enter()
        {
            Debug.Log("Moving Piece");

            _chosenPieceText.text = StateMachine.BoardManager.CurrentButton.name;
            _pieceControlsText.gameObject.SetActive(true);

            StateMachine.Input.PressedUp += MovePieceUp;
            StateMachine.Input.PressedDiagonalLeft += MovePieceDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight += MovePieceDiagonalRight;
            StateMachine.Input.PressedJump += MovePieceJump;
            StateMachine.Input.PressedConfirm += ConfirmPiece;
            StateMachine.Input.PressedCancel += UndoPiece;
        }

        public override void Exit()
        {
            Debug.Log("Moved Piece");

            _chosenPieceText.text = "None";
            _pieceControlsText.gameObject.SetActive(false);

            StateMachine.Input.PressedUp -= MovePieceUp;
            StateMachine.Input.PressedDiagonalLeft -= MovePieceDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight -= MovePieceDiagonalRight;
            StateMachine.Input.PressedJump -= MovePieceJump;
            StateMachine.Input.PressedConfirm -= ConfirmPiece;
            StateMachine.Input.PressedCancel -= UndoPiece;
        }

        public void MovePieceUp()
        {
            ICommand moveUpCommand = new MoveUpCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveUpCommand);
        }

        public void MovePieceDiagonalLeft()
        {
            ICommand moveDiagonalLeft = new MoveDiagonalUpLeftCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalLeft);
        }

        public void MovePieceDiagonalRight()
        {
            ICommand moveDiagonalRight = new MoveDiagonalUpRightCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalRight);
        }

        public void MovePieceJump()
        {
            ICommand moveJumpCommand = new MoveJumpUpCommand(StateMachine.BoardManager.CurrentButton);
            _commandStack.ExecuteCommand(moveJumpCommand);
        }

        public void ConfirmPiece()
        {
            if (StateMachine.BoardManager.CurrentButton.GetComponent<GamePiece>()._moved) {
                foreach (GameObject pieces in StateMachine.BoardManager.PlayerPieces)
                {
                    Button button = pieces.GetComponent<Button>();
                    button.interactable = false;
                }
                StateMachine.ChangeState<EnemyTurnBoardGameState>();
                StateMachine.BoardManager.CurrentButton.GetComponent<GamePiece>()._moved = false;
            }
        }

        public void UndoPiece()
        {
            GamePiece piece = StateMachine.BoardManager.CurrentButton.GetComponent<GamePiece>();
            if (piece._moved)
            {
                piece.UndoMove();
            }
            else
            {
                foreach (GameObject pieces in StateMachine.BoardManager.PlayerPieces)
                {
                    Button button = pieces.GetComponent<Button>();
                    button.interactable = false;
                    button.interactable = true;
                }
                StateMachine.ChangeState<PlayerSelectingPieceState>();
            }
        }
    }
}
