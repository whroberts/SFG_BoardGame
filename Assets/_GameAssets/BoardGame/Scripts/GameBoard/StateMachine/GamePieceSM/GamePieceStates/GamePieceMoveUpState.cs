using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceMoveUpState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Moving Piece Up");
            StartCoroutine(MoveUp(StateMachine.BoardManager.CurrentButton));
            StateMachine.Input.PressedCancel += OnCancelCurrentPiece;
        }

        public override void Exit()
        {
            Debug.Log("Moved Piece Up");
            StateMachine.Input.PressedCancel -= OnCancelCurrentPiece;
        }

        IEnumerator MoveUp(Button button)
        {
            Vector2 moveToPosition = new Vector2(button.transform.position.x, button.transform.position.y + 100);
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
