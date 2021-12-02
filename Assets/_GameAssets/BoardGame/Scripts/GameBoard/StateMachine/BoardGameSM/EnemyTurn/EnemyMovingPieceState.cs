using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BoardGame
{
    public class EnemyMovingPieceState : BoardGameState
    {
        [SerializeField] TMP_Text _chosenPieceText = null;

        CommandStack _commandStack = new CommandStack();

        public override void Enter()
        {
            Debug.Log("Enemy Moving Piece");
            _chosenPieceText.text = StateMachine.BoardManager.EnemyCurrentButton.name;

            int move = Random.Range(0, 3);

            if (move == 0)
            {
                MovePieceDown();
            }
            else if (move == 1)
            {
                MovePieceDiagonalDownLeft();
            }
            else if (move == 2)
            {
                MovePieceDiagonalDownRight();
            }
        }

        public override void Tick()
        {
            ChooseDifferntPiece();
            ConfirmPiece();
        }

        public override void Exit()
        {
            _chosenPieceText.text = "None";
            Debug.Log("Enemy Moved Piece");
        }

        public void MovePieceDown()
        {
            ICommand moveDownCommand = new MoveDownCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveDownCommand);
        }

        public void MovePieceDiagonalDownLeft()
        {
            ICommand moveDiagonalDownLeft = new MoveDiagonalDownLeftCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalDownLeft);
        }

        public void MovePieceDiagonalDownRight()
        {
            ICommand moveDiagonalDownRight = new MoveDiagonalDownRightCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveDiagonalDownRight);
        }

        public void MovePieceJump()
        {
            ICommand moveJumpCommand = new MoveJumpUpCommand(StateMachine.BoardManager.PlayerCurrentButton);
            _commandStack.ExecuteCommand(moveJumpCommand);
        }
        
        public void ChooseDifferntPiece()
        {
            if (StateMachine.BoardManager.EnemyCurrentButton.GetComponent<GamePiece>()._cantMove)
            {
                StateMachine.ChangeState<EnemySelectingPieceState>();
            }
        }

        public void ConfirmPiece()
        {
            if (StateMachine.BoardManager.EnemyCurrentButton.GetComponent<GamePiece>()._moved && 
                !StateMachine.BoardManager.EnemyCurrentButton.GetComponent<GamePiece>()._cantMove)
            {
                /*
                foreach (GameObject pieces in StateMachine.BoardManager.EnemyPieceList)
                {
                    Button button = pieces.GetComponent<Button>();
                    GamePiece script = pieces.GetComponent<GamePiece>();
                    script._cantMove = false;
                    script._moved = false;
                    button.interactable = false;
                    
                }
                */

                StartCoroutine(DelayOnFoundValidMovement());

                StateMachine.BoardManager.EnemyCurrentButton.GetComponent<GamePiece>()._moved = false;
                StartCoroutine(StateMachine.BoardManager.Attacked());
                StateMachine.BoardManager.EnemyCurrentButton.GetComponent<GamePiece>()._audioSource.Play();

                //StateMachine.ChangeState<PlayerSelectingPieceState>();
                StartCoroutine(DelayForPlayerChange());
            }
        }

        private IEnumerator DelayOnFoundValidMovement()
        {
            yield return new WaitForSeconds(1f);
            foreach (GameObject pieces in StateMachine.BoardManager.EnemyPieceList)
            {
                Button button = pieces.GetComponent<Button>();
                GamePiece script = pieces.GetComponent<GamePiece>();
                script._cantMove = false;
                script._moved = false;
                button.interactable = false;

            }

            yield return new WaitForSeconds(1f);
        }

        private IEnumerator DelayForPlayerChange()
        {
            yield return new WaitForSeconds(1.25f);
            StateMachine.Input.AllowPlayerInputs = true;
            StateMachine.ChangeState<PlayerSelectingPieceState>();
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
                StateMachine.ChangeState<EnemySelectingPieceState>();
            }
        }
    }
}
