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
            Debug.Log("Move Up Enter");
            StartCoroutine(MoveUp(StateMachine.BoardManager.CurrentButton));
        }

        public override void Exit()
        {
            Debug.Log("Move Up Exit");
            StateMachine.Input.PressedConfirm -= EndTurn;
        }

        IEnumerator MoveUp(Button button)
        {
            Vector2 moveToPosition = new Vector2(button.transform.position.x, button.transform.position.y + 100);
            Vector2 savedPosition = button.transform.position;

            button.transform.position = moveToPosition;

            yield return new WaitForSeconds(0.1f);
            if (!StateMachine.BoardManager.MoveLocationCheck(button, moveToPosition))
            {
                yield return new WaitForSeconds(0.1f);
                button.transform.position = moveToPosition;
                StateMachine.Input.PressedConfirm += EndTurn;
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                button.transform.position = savedPosition;
                EndTurn();
            }
        }

        private void EndTurn()
        {

            foreach (GameObject piece in StateMachine.BoardManager.PlayerPieces)
            {
                Button button = piece.GetComponent<Button>();
                button.interactable = false;
            }
            StateMachine.ChangeState<GamePieceTransitionState>();
        }
    }
}
