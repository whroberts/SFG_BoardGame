using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceMoveJumpState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Jumping Piece");
            StartCoroutine(MoveJump(StateMachine.BoardManager.CurrentButton));
            StateMachine.Input.PressedConfirm += OnCancelCurrentPiece;
        }

        public override void Exit()
        {
            Debug.Log("Jumped Piece");
            StateMachine.Input.PressedConfirm -= OnCancelCurrentPiece;
        }

        IEnumerator MoveJump(Button button)
        {
            Vector2 moveToPosition = new Vector2(button.transform.position.x, button.transform.position.y + 200);
            Vector2 savedPosition = button.transform.position;

            button.transform.position = moveToPosition;

            yield return new WaitForSeconds(0.1f);

            //StartCoroutine(Turn(StateMachine.BoardManager.MovePiece(button, moveToPosition, savedPosition)));
        }

        private IEnumerator Turn(bool moveState)
        {
            if (moveState)
            {
                foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
                {
                    Button button = piece.GetComponent<Button>();
                    button.interactable = false;
                }

                yield return new WaitForSeconds(0.5f);
                StateMachine.ChangeState<GamePieceTransitionState>();
            }
            else if (!moveState)
            {
                yield return new WaitForSeconds(0.1f);
                StateMachine.ChangeState<GamePieceIdleState>();
            }
        }

        private void OnCancelCurrentPiece()
        {
            StateMachine.ChangeState<GamePieceIdleState>();
        }
    }
}
