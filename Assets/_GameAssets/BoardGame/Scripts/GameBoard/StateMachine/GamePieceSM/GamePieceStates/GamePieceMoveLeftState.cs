using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceMoveLeftState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Moving Piece Left");
            StartCoroutine(MoveLeft(StateMachine.BoardManager.CurrentButton));
            StateMachine.Input.PressedConfirm += OnCancelCurrentPiece;
        }

        public override void Exit()
        {
            Debug.Log("Moved Piece Left");
            StateMachine.Input.PressedConfirm -= OnCancelCurrentPiece;
        }

        IEnumerator MoveLeft(Button button)
        {
            Vector2 moveToPosition = new Vector2(button.transform.position.x - 100, button.transform.position.y);
            Vector2 savedPosition = button.transform.position;

            button.transform.position = moveToPosition;

            yield return new WaitForSeconds(0.1f);

            Turn(StateMachine.BoardManager.MovePiece(button, moveToPosition, savedPosition));

            yield return new WaitForSeconds(0.1f);
        }

        private void Turn(bool moveState)
        {
            if (moveState)
            {
                foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
                {
                    Button button = piece.GetComponent<Button>();
                    button.interactable = false;
                }

                StateMachine.ChangeState<GamePieceTransitionState>();
            }
            else if (!moveState)
            {
                StateMachine.ChangeState<GamePieceIdleState>();
            }
        }

        private void OnCancelCurrentPiece()
        {
            StateMachine.ChangeState<GamePieceIdleState>();
        }
    }
}
