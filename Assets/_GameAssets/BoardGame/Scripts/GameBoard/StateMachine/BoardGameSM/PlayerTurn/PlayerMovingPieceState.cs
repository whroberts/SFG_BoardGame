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
            //Debug.Log("Player Moving Piece");

            _chosenPieceText.text = StateMachine.BoardManager.PlayerCurrentButton.name;
            _pieceControlsText.gameObject.SetActive(true);


            StateMachine.Input.PressedUp += MovePieceUp;
            StateMachine.Input.PressedJumpDiagonalUpLeft += MovePieceJumpDiagonalUpLeft;
            StateMachine.Input.PressedJumpDiagonalUpRight += MovePieceJumpDiagonalUpRight;
            StateMachine.Input.PressedDiagonalLeft += MovePieceDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight += MovePieceDiagonalRight;
            StateMachine.Input.PressedJump += MovePieceJump;
            StateMachine.Input.PressedConfirm += ConfirmPiece;
            StateMachine.Input.PressedCancel += UndoPiece;
        }

        public override void Exit()
        {
            //Debug.Log("Player Moved Piece");

            _chosenPieceText.text = "None";
            _pieceControlsText.gameObject.SetActive(false);

            StateMachine.Input.PressedUp -= MovePieceUp;
            StateMachine.Input.PressedJumpDiagonalUpLeft -= MovePieceJumpDiagonalUpLeft;
            StateMachine.Input.PressedJumpDiagonalUpRight -= MovePieceJumpDiagonalUpRight;
            StateMachine.Input.PressedDiagonalLeft -= MovePieceDiagonalLeft;
            StateMachine.Input.PressedDiagonalRight -= MovePieceDiagonalRight;
            StateMachine.Input.PressedJump -= MovePieceJump;
            StateMachine.Input.PressedConfirm -= ConfirmPiece;
            StateMachine.Input.PressedCancel -= UndoPiece;
        }

        public void MovePieceUp()
        {
            ICommand moveUpCommand = new MoveUpCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveUpCommand);
        }

        public void MovePieceDiagonalLeft()
        {
            ICommand moveDiagonalLeft = new MoveDiagonalUpLeftCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalLeft);
        }

        public void MovePieceDiagonalRight()
        {
            ICommand moveDiagonalRight = new MoveDiagonalUpRightCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalRight);
        }
        

        public void MovePieceJump()
        {
            ICommand moveJumpCommand = new MoveJumpUpCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveJumpCommand);
        }

        public void MovePieceJumpDiagonalUpLeft()
        {
            ICommand moveJumpDiagonalLeft = new MoveJumpUpCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveJumpDiagonalLeft);
        }

        public void MovePieceJumpDiagonalUpRight()
        {
            ICommand moveJumpDiagonalRight = new MoveJumpUpCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveJumpDiagonalRight);
        }
        

        public void ConfirmPiece()
        {
            if (StateMachine.BoardManager.PlayerCurrentButton.GetComponent<GamePiece>()._moved) {
                foreach (GameObject pieces in StateMachine.BoardManager.PlayerPieceList)
                {
                    Button button = pieces.GetComponent<Button>();
                    button.interactable = false;
                }
                StateMachine.BoardManager.PlayerCurrentButton.GetComponent<GamePiece>()._moved = false;
                StateMachine.BoardManager.Attacked();

                StateMachine.ChangeState<EnemySelectingPieceState>();
            }
        }

        public void UndoPiece()
        {
            GamePiece piece = StateMachine.BoardManager.PlayerCurrentButton.GetComponent<GamePiece>();
            if (piece._moved)
            {
                piece.UndoMove();
            }
            else
            {
                foreach (GameObject pieces in StateMachine.BoardManager.PlayerPieceList)
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
