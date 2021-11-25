using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceMoveDiagonalRightState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Moving Piece DiagonalRight");
            StartCoroutine(MoveDiagonalRight(StateMachine.BoardManager.PlayerCurrentButton));
            StateMachine.Input.PressedCancel += OnCancelCurrentPiece;
        }

        public override void Exit()
        {
            Debug.Log("Moved Piece DiagonalRight");
            StateMachine.Input.PressedCancel -= OnCancelCurrentPiece;
        }

        IEnumerator MoveDiagonalRight(Button button)
        {
            Vector2 moveToPosition = new Vector2(button.transform.position.x + 100, button.transform.position.y + 100);
            Vector2 savedPosition = button.transform.position;

            button.transform.position = moveToPosition;

            yield return new WaitForSeconds(0.1f);

            //StartCoroutine(Turn(StateMachine.BoardManager.MovePiece(button, moveToPosition, savedPosition)));
        }

        private IEnumerator Turn(bool moveState)
        {
            if (moveState)
            {
                foreach (GameObject piece in StateMachine.BoardManager.PlayerPieceList)
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
