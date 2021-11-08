using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoardGame
{
    public class EnemyMovingPieceState : BoardGameState
    {
        [SerializeField] TMP_Text _enemyThinkingTextUI = null;

        CommandStack _commandStack = new CommandStack();

        public override void Enter()
        {
            Debug.Log("Enemy Moving Piece");
            MovePieceDown();
        }

        public override void Exit()
        {
            _enemyThinkingTextUI.gameObject.SetActive(false);
            Debug.Log("Enemy Moved Piece");
        }

        public void MovePieceDown()
        {
            ICommand moveDownCommand = new MoveDownCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveDownCommand);
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

        public void ConfirmPiece()
        {
            if (StateMachine.BoardManager.EnemyCurrentButton.GetComponent<GamePiece>()._moved)
            {
                foreach (GameObject pieces in StateMachine.BoardManager.EnemyPieces)
                {
                    Button button = pieces.GetComponent<Button>();
                    button.interactable = false;
                }
                StateMachine.BoardManager.EnemyCurrentButton.GetComponent<GamePiece>()._moved = false;
                StateMachine.ChangeState<PlayerSelectingPieceState>();
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
                foreach (GameObject pieces in StateMachine.BoardManager.PlayerPieces)
                {
                    Button button = pieces.GetComponent<Button>();
                    button.interactable = false;
                    button.interactable = true;
                }
                StateMachine.ChangeState<EnemySelectingPieceState>();
            }
        }
    }
}
