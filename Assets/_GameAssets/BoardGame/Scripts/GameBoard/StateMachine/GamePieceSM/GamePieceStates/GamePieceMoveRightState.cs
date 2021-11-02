using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BoardGame
{
    public class GamePieceMoveRightState : GamePieceState
    {
        public override void Enter()
        {
            Debug.Log("Move Right Enter");
            MoveRight(StateMachine.BoardManager.CurrentButton);
            StateMachine.Input.PressedConfirm += EndTurn;
        }

        public override void Exit()
        {
            Debug.Log("Move Right Exit");
            StateMachine.Input.PressedConfirm -= EndTurn;
        }

        void MoveRight(Button button)
        {
            button.transform.position = new Vector3(button.transform.position.x + 100, button.transform.position.y, button.transform.position.z);
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
