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
            Debug.Log("Move DiagonalRight Enter");
            MoveDiagonalRight(StateMachine.BoardManager.CurrentButton);
            StateMachine.Input.PressedConfirm += EndTurn;
        }

        public override void Exit()
        {
            Debug.Log("Move DiagonalRight Exit");
            StateMachine.Input.PressedConfirm -= EndTurn;
        }

        void MoveDiagonalRight(Button button)
        {
            button.transform.position = new Vector3(button.transform.position.x + 100, button.transform.position.y + 100, button.transform.position.z);
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
